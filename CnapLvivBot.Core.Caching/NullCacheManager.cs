using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CnapLvivBot.Core.Caching
{
	/// <summary>
	///     Represents a NullCache
	/// </summary>
	public class NullCacheManager : ICacheManager
	{
		#region Sync

		/// <summary>
		///     Gets or sets the value associated with the specified key.
		/// </summary>
		/// <typeparam name="T">Type</typeparam>
		/// <param name="key">The key of the value to get.</param>
		/// <returns>The value associated with the specified key.</returns>
		public T Get<T>(string key) where T : class
		{
			return default(T);
		}

		public IList<T> Get<T>(IList<string> keys) where T : class
		{
			return default(IList<T>);
		}

		public IList<T> GetAll<T>(string generate) where T : class
		{
			throw new NotImplementedException();
		}

		public void Set<T>(string key, T data, TimeSpan? cacheTime) where T : class
		{
		}

		public void Set<T>(IList<Tuple<string, T>> keyDataDictionary) where T : class
		{
		}

		/// <summary>
		///     Gets a value indicating whether the value associated with the specified key is cached
		/// </summary>
		/// <param name="key">key</param>
		/// <returns>Result</returns>
		public bool IsSet(string key)
		{
			return false;
		}

		/// <summary>
		///     Removes the value with the specified key from the cache
		/// </summary>
		/// <param name="key">/key</param>
		public void Remove(string key)
		{
		}

		public void RemoveAll(IList<string> keys)
		{
			throw new NotImplementedException();
		}


		/// <summary>
		///     Removes items by pattern
		/// </summary>
		/// <param name="pattern">pattern</param>
		public void RemoveByPattern(string pattern)
		{
		}

		/// <summary>
		///     Clear all cache data
		/// </summary>
		public void Clear()
		{
		}

		public Dictionary<string, object> GetAll()
		{
			return new Dictionary<string, object>();
		}

		public bool IsUp()
		{
			return true;
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

		#endregion
	}
}