using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;

namespace CnapLvivBot.Core.Caching.Redis
{
	public static class StackExchangeRedisCacheClientExtensions
	{
		public static bool Add<T>(this StackExchangeRedisCacheClient cacheClient, string key, T value, TimeSpan expiresIn,
			bool fireAndForget)
		{
			var numArray = cacheClient.Serializer.Serialize(value);
			return cacheClient.Database.StringSet(key, numArray, expiresIn, When.Always,
				fireAndForget ? CommandFlags.FireAndForget : CommandFlags.None);
		}

		public static async Task<bool> AddAsync<T>(this StackExchangeRedisCacheClient cacheClient, string key, T value,
			TimeSpan expiresIn, bool fireAndForget)
		{
			var numArray = await cacheClient.Serializer.SerializeAsync(value);
			var res = await cacheClient.Database.StringSetAsync(key, numArray, expiresIn, When.Always,
				fireAndForget ? CommandFlags.FireAndForget : CommandFlags.None);
			return res;
		}

		public static bool AddAll<T>(this StackExchangeRedisCacheClient cacheClient, IList<Tuple<string, T>> items,
			DateTimeOffset? expiresAt, TimeSpan? expiresIn, bool fireAndForget)
		{
			if (expiresAt.HasValue || expiresIn.HasValue)
				throw new NotImplementedException();

			var redisKeyArray = new RedisKey[items.Count];
			var redisValueArray = new RedisValue[items.Count];
			var result = cacheClient.Database.ScriptEvaluate(
				CreateLuaScriptForMset(redisKeyArray, redisValueArray, items, cacheClient.Serializer), redisKeyArray,
				redisValueArray, fireAndForget ? CommandFlags.FireAndForget : CommandFlags.None);
			if (!fireAndForget)
				return result != null && result.ToString() == "OK";
			return true;
		}

		public static async Task<bool> AddAllAsync<T>(this StackExchangeRedisCacheClient cacheClient,
			IList<Tuple<string, T>> items, DateTimeOffset? expiresAt, TimeSpan? expiresIn, bool fireAndForget)
		{
			if (expiresAt.HasValue || expiresIn.HasValue)
				throw new NotImplementedException();

			var redisKeyArray = new RedisKey[items.Count];
			var redisValueArray = new RedisValue[items.Count];

			var result = await cacheClient.Database.ScriptEvaluateAsync(
				CreateLuaScriptForMset(redisKeyArray, redisValueArray, items, cacheClient.Serializer), redisKeyArray,
				redisValueArray, fireAndForget ? CommandFlags.FireAndForget : CommandFlags.None);

			if (!fireAndForget)
				return result != null && result.ToString() == "OK";
			return true;
		}

		public static void SetAdd(this StackExchangeRedisCacheClient cacheClient, string key, IEnumerable<string> values)
		{
			cacheClient.Database.SetAdd(key, values.Select(x => (RedisValue) x).ToArray());
		}

		public static Task SetAddAsync(this StackExchangeRedisCacheClient cacheClient, string key, IEnumerable<string> values)
		{
			return cacheClient.Database.SetAddAsync(key, values.Select(x => (RedisValue) x).ToArray());
		}

		public static void SetRemove(this StackExchangeRedisCacheClient cacheClient, string key, string value)
		{
			cacheClient.Database.SetRemove(key, value);
		}

		public static Task SetRemoveAsync(this StackExchangeRedisCacheClient cacheClient, string key, string value)
		{
			return cacheClient.Database.SetRemoveAsync(key, value);
		}

		public static void SetRemove(this StackExchangeRedisCacheClient cacheClient, string key, IEnumerable<string> values)
		{
			cacheClient.Database.SetRemove(key, values.Select(x => (RedisValue) x).ToArray());
		}

		public static Task SetRemoveAsync(this StackExchangeRedisCacheClient cacheClient, string key,
			IEnumerable<string> values)
		{
			return cacheClient.Database.SetRemoveAsync(key, values.Select(x => (RedisValue) x).ToArray());
		}

		public static void RemoveAllKeys(this StackExchangeRedisCacheClient cacheClient, IEnumerable<string> keys,
			bool fireAndForget = false)
		{
			cacheClient.Database.KeyDelete(keys.Select(x => (RedisKey) x).ToArray(),
				fireAndForget ? CommandFlags.FireAndForget : CommandFlags.None);
		}

		public static Task RemoveAllKeysAsync(this StackExchangeRedisCacheClient cacheClient, IEnumerable<string> keys,
			bool fireAndForget = false)
		{
			return cacheClient.Database.KeyDeleteAsync(keys.Select(x => (RedisKey) x).ToArray(),
				fireAndForget ? CommandFlags.FireAndForget : CommandFlags.None);
		}

		public static Dictionary<string, object> GetAllPropperAsDictionary<T>(this StackExchangeRedisCacheClient cacheClient,
			IEnumerable<string> keys) where T : class
		{
			var keysList = keys.ToList();
			if (!keysList.Any())
				return new Dictionary<string, object>();

			var redisKeyArray = new RedisKey[keysList.Count];
			var redisResultArray =
				(RedisResult[]) cacheClient.Database.ScriptEvaluate(CreateLuaScriptForMget(redisKeyArray, keysList), redisKeyArray);
			var result = new Dictionary<string, object>();

			for (var index = 0; index < redisResultArray.Count(); ++index)
				if (!redisResultArray[index].IsNull)
				{
					var obj = cacheClient.Serializer.Deserialize<T>((byte[]) redisResultArray[index]);
					result.Add(keysList[index], obj);
				}
			return result;
		}

		private static string CreateLuaScriptForMget(IList<RedisKey> redisKeys, IReadOnlyList<string> keysList)
		{
			var stringBuilder = new StringBuilder("return redis.call('mget',");
			for (var index = 0; index < keysList.Count; ++index)
			{
				redisKeys[index] = keysList[index];
				stringBuilder.AppendFormat("KEYS[{0}]", index + 1);
				if (index < keysList.Count - 1)
					stringBuilder.Append(",");
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		private static string CreateLuaScriptForMset<T>(IList<RedisKey> redisKeys, IList<RedisValue> redisValues,
			IList<Tuple<string, T>> objects, ISerializer serializer)
		{
			var stringBuilder = new StringBuilder("return redis.call('mset',");
			for (var index = 0; index < objects.Count; ++index)
			{
				redisKeys[index] = objects[index].Item1;
				redisValues[index] = serializer.Serialize(objects[index].Item2);
				stringBuilder.AppendFormat("KEYS[{0}],ARGV[{0}]", index + 1);
				if (index < objects.Count - 1)
					stringBuilder.Append(",");
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}
	}
}