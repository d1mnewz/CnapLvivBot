using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.MsgPack;

namespace CnapLvivBot.Core.Caching.Redis
{
	[Serializable]
	public sealed class RedisCacheManager : ICacheManager
	{
		[NonSerialized] private readonly StackExchangeRedisCacheClient _cacheClient;
		[NonSerialized] private readonly TimeSpan _cacheTime;
		[NonSerialized] private readonly ConnectionMultiplexer _redis;
		[NonSerialized] private readonly RetryPolicy _retryPolicy;

		public RedisCacheManager(string connectionString, TimeSpan? cacheTime = null, int errorRetryCount = 0,
			TimeSpan? errorRetryInterval = null)
		{
			_redis = ConnectionMultiplexer.Connect(connectionString);
			_cacheClient = new StackExchangeRedisCacheClient(_redis, new MsgPackObjectSerializer());

			var retryStrategy = new FixedInterval(errorRetryCount, errorRetryInterval ?? TimeSpan.Zero);
			_retryPolicy = new RetryPolicy<RedisCacheTransientErrorDetectionStrategy>(retryStrategy);
			_cacheTime = cacheTime ?? TimeSpan.FromDays(7);
		}

		#region Sync

		public T Get<T>(string key) where T : class
		{
			return _retryPolicy.ExecuteAction(() => _cacheClient.Get<T>(key));
		}

		IList<T> ICacheManager.Get<T>(IList<string> keys)
		{
			var redisKeys = new RedisKey[keys.Count];
			for (var i = 0; i < keys.Count; i++)
				redisKeys[i] = keys[i];
			return _cacheClient.Database.StringGet(redisKeys).Where(x => x.HasValue).Select(redisValue =>
				_cacheClient.Serializer.Deserialize<T>(redisValue)).ToList();
		}

		public IList<T> GetAll<T>(string generate) where T : class
		{

			//var result = new List<T>();
			//var dummy = _cacheClient.SearchKeys(generate + "*");
			//foreach (var key in dummy)
			//{
			//	result.Add(_cacheClient.Get<T>(key));
			//}
			//return result;
			return _cacheClient.GetAll<T>(generate, 100); // TODO:
		}


		public void Set<T>(string key, T data, TimeSpan? cacheTime = null) where T : class
		{
			_retryPolicy.ExecuteAction(() => _cacheClient.Add(key, data, cacheTime ?? _cacheTime, false));
		}

		public void Set<T>(IList<Tuple<string, T>> keyData) where T : class
		{
			_retryPolicy.ExecuteAction(() => _cacheClient.AddAll(keyData));
		}

		public bool IsSet(string key)
		{
			return _retryPolicy.ExecuteAction(() => _cacheClient.Exists(key));
		}

		public void Remove(string key)
		{
			_retryPolicy.ExecuteAction(() => _cacheClient.Remove(key));
		}

		public void RemoveAll(IList<string> keys)
		{
			_retryPolicy.ExecuteAction(() => _cacheClient.RemoveAll(keys));
		}

		public void RemoveByPattern(string pattern)
		{
			_retryPolicy.ExecuteAction(() =>
			{
				var result = _cacheClient.Database.ScriptEvaluate("return redis.call('del', unpack(redis.call('keys', ARGV[1])))",
					null,
					new RedisValue[] { pattern });
				try
				{
					return Convert.ToInt32(result.ToString().Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0]);
				}
				catch (Exception)
				{
					return 0;
				}
			});
		}

		public void Clear()
		{
			//_cacheClient.FlushDb() will throw 'This operation is not available unless admin mode is enabled: FLUSHDB' ... so enable first or just dont use it
			_retryPolicy.ExecuteAction(() => _cacheClient.FlushDb());
		}

		public Dictionary<string, object> GetAll()
		{
			throw new NotSupportedException();
		}

		public bool IsUp()
		{
			try
			{
				var key = $"tmp_{Guid.NewGuid()}{Guid.NewGuid()}";
				var testObject = $"testobject_{Guid.NewGuid()}";

				_cacheClient.Add(key, testObject);

				var result = _cacheClient.Get<string>(key);
				return string.Equals(result, testObject, StringComparison.InvariantCultureIgnoreCase);
			}
			catch
			{
				// ignored
			}
			return false;
		}

		public IDisposable AcquireLock(string key, TimeSpan? timeout = null, TimeSpan? expireTime = null,
			TimeSpan? retryTime = null)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region Async

		public Task<T> GetAsync<T>(string key) where T : class
		{
			return _retryPolicy.ExecuteAsync(() => _cacheClient.GetAsync<T>(key));
		}

		public async Task SetAsync<T>(string key, T data, TimeSpan? cacheTime = null) where T : class
		{
			await _retryPolicy.ExecuteAsync(() => _cacheClient.AddAsync(key, data, cacheTime ?? _cacheTime, false));
		}

		public Task<bool> IsSetAsync(string key)
		{
			return _retryPolicy.ExecuteAsync(() => _cacheClient.ExistsAsync(key));
		}

		public async Task RemoveAsync(string key)
		{
			await _retryPolicy.ExecuteAsync(() => _cacheClient.RemoveAsync(key));
		}

		public Task RemoveByPatternAsync(string pattern)
		{
			return _retryPolicy.ExecuteAsync(() =>
			{
				var result = _cacheClient.Database.ScriptEvaluate("return redis.call('del', unpack(redis.call('keys', ARGV[1])))",
					null,
					new RedisValue[] { pattern });
				try
				{
					return Task.FromResult(
						Convert.ToInt32(result.ToString().Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0]));
				}
				catch (Exception)
				{
					return Task.FromResult(0);
				}
			});
		}

		public Task ClearAsync()
		{
			return _retryPolicy.ExecuteAsync(() => _cacheClient.FlushDbAsync());
		}

		public Task<Dictionary<string, object>> GetAllAsync()
		{
			throw new NotSupportedException();
		}

		public async Task<bool> IsUpAsync()
		{
			try
			{
				var key = $"tmp_{Guid.NewGuid()}{Guid.NewGuid()}";
				var testObject = $"testobject_{Guid.NewGuid()}";

				await _cacheClient.AddAsync(key, testObject);

				var result = await _cacheClient.GetAsync<string>(key);
				return string.Equals(result, testObject, StringComparison.InvariantCultureIgnoreCase);
			}
			catch
			{
				// ignored
			}
			return false;
		}

		#endregion
	}
}