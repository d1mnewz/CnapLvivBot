using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CnapLvivBot.Core.Caching
{
	/// <summary>
	///     Cache Extensions
	/// </summary>
	public static class CacheExtensions
	{
		public static string GetCacheKeyPattern<T>()
		{
			return typeof(T).FullName + "-{0}";
		}

		public static string GetCacheKey<T>(object id)
		{
			return GetCacheKey(GetCacheKeyPattern<T>(), id);
		}

		public static string GetCacheKey(string pattern, object id)
		{
			return string.Format(pattern, id);
		}

		#region Async

		public static async Task<T> GetAsync<T>(this ICacheManager cm, string key, Func<Task<T>> acuire,
			TimeSpan? cacheTime = null) where T : class
		{
			var val = await cm.GetAsync<T>(key);
			if (Comparer<T>.Default.Compare(val, default(T)) != 0)

				return val;

			var result = await acuire();
			if (Comparer<T>.Default.Compare(result, default(T)) != 0)

				await cm.SetAsync(key, result, cacheTime);

			return result;
		}

		#endregion

		#region Sync

		public static T Get<T>(this ICacheManager cacheManager, string key, Func<T> acquire, TimeSpan? cacheTime = null)
			where T : class
		{
			var val = cacheManager.Get<T>(key);
			if (Comparer<T>.Default.Compare(val, default(T)) != 0)

				return val;

			var result = acquire();
			if (Comparer<T>.Default.Compare(result, default(T)) != 0)

				cacheManager.Set(key, result, cacheTime);

			return result;
		}

		public static T GetNotNull<T>(this ICacheManager cacheManager, string key, Func<T> acquire,
			TimeSpan? cacheTime = null) where T : class
		{
			return cacheManager.Get(key, acquire, cacheTime);
		}

		public static void Set<T>(this ICacheManager cacheManager, string key, T data, TimeSpan? cacheTime = null)
			where T : class
		{
			cacheManager.Set(key, data, cacheTime);
		}

		#endregion
	}
}