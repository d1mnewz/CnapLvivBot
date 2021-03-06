﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace CnapLvivBot.Core.Caching
{
	/// <summary>
	///     Represents a MemoryCacheCache
	/// </summary>
	public class MemoryCacheManager : ICacheManager
	{
		private readonly TimeSpan _defaultObjectCacheTime;

		public MemoryCacheManager(TimeSpan defaultObjectCacheTime)
		{
			_defaultObjectCacheTime = defaultObjectCacheTime;
		}

		protected ObjectCache Cache => MemoryCache.Default;

		#region Sync

		/// <summary>
		///     Gets or sets the value associated with the specified key.
		/// </summary>
		/// <typeparam name="T">Type</typeparam>
		/// <param name="key">The key of the value to get.</param>
		/// <returns>The value associated with the specified key.</returns>
		public T Get<T>(string key) where T : class
		{
			return (T) Cache[key];
		}

		public IList<T> GetAll<T>(string generate) where T : class
		{
			throw new NotImplementedException();
		}

		public void Set<T>(string key, T data, TimeSpan? cacheTime) where T : class
		{
			if (data is null)
				return;

			var policy = new CacheItemPolicy {AbsoluteExpiration = DateTime.Now + (cacheTime ?? _defaultObjectCacheTime)};
			Cache.Set(new CacheItem(key, data), policy);
		}

		public void Set<T>(IList<Tuple<string, T>> keyDataDictionary) where T : class
		{
			throw new NotImplementedException();
		}


		/// <summary>
		///     Gets a value indicating whether the value associated with the specified key is cached
		/// </summary>
		/// <param name="key">key</param>
		/// <returns>Result</returns>
		public bool IsSet(string key)
		{
			return Cache.Contains(key);
		}

		/// <summary>
		///     Removes the value with the specified key from the cache
		/// </summary>
		/// <param name="key">/key</param>
		public void Remove(string key)
		{
			Remove(key, key);
		}

		public void RemoveAll(IList<string> keys)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		///     Removes the value with the specified key from the cache
		/// </summary>
		/// <param name="key">/key</param>
		/// <param name="keyFormated"></param>
		private void Remove(string key, string keyFormated)
		{
			Cache.Remove(keyFormated);
		}

		/// <summary>
		///     Removes items by pattern
		/// </summary>
		/// <param name="pattern">pattern</param>
		public void RemoveByPattern(string pattern)
		{
			var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
			var keysToRemove = (from item in Cache where regex.IsMatch(item.Key) select item.Key).ToList();

			foreach (var keyFormated in keysToRemove)
				Remove(null, keyFormated);
		}

		/// <summary>
		///     Clear all cache data
		/// </summary>
		public void Clear()
		{
			foreach (var item in Cache)
				Remove(null, item.Key);
		}

		public Dictionary<string, object> GetAll()
		{
			return MemoryCache.Default.ToDictionary(x => x.Key, x => x.Value);
		}

		public bool IsUp()
		{
			return true;
		}

		private readonly ConcurrentDictionary<string, SemaphoreSlim> _semaphores =
			new ConcurrentDictionary<string, SemaphoreSlim>();

		private class CacheLock : IDisposable
		{
			private readonly string _key;
			private readonly SemaphoreSlim _semaphore;
			private readonly ConcurrentDictionary<string, SemaphoreSlim> _semaphores;

			public CacheLock(ConcurrentDictionary<string, SemaphoreSlim> semaphores, SemaphoreSlim semaphore, string key)
			{
				_semaphores = semaphores;
				_key = key;
				_semaphore = semaphore;
			}

			public void Dispose()
			{
				_semaphore.Release();
				_semaphores.TryRemove(_key, out var s);
			}
		}

		#endregion

		#region Async

		public Task<T> GetAsync<T>(string key) where T : class
		{
			return Task.FromResult(Get<T>(key));
		}

		public Task SetAsync<T>(string key, T data, TimeSpan? cacheTime) where T : class
		{
			Set(key, data, cacheTime);
			return Task.FromResult(true);
		}

		public Task<bool> IsSetAsync(string key)
		{
			return Task.FromResult(IsSet(key));
		}

		public Task RemoveAsync(string key)
		{
			Remove(key);
			return Task.FromResult(true);
		}

		public Task RemoveByPatternAsync(string pattern)
		{
			RemoveByPattern(pattern);
			return Task.FromResult(true);
		}

		public Task ClearAsync()
		{
			Clear();
			return Task.FromResult(true);
		}

		public Task<Dictionary<string, object>> GetAllAsync()
		{
			return Task.FromResult(GetAll());
		}

		public Task<bool> IsUpAsync()
		{
			return Task.FromResult(IsUp());
		}

		public IList<T> Get<T>(IList<string> keys) where T : class
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}