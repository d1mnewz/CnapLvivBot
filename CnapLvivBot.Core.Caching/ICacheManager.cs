using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CnapLvivBot.Core.Caching
{
	public interface ICacheManager
	{
		#region Sync

		/// <summary>
		///     Gets or sets the value associated with the specified key.
		/// </summary>
		/// <typeparam name="T">Type</typeparam>
		/// <param name="key">The key of the value to get.</param>
		/// <returns>The value associated with the specified key.</returns>
		T Get<T>(string key) where T : class;

		///// <summary>
		///// Gets the values associated with specified keys.
		///// </summary>
		///// <typeparam name="T">Type</typeparam>
		///// <param name="keys">The keys of values to get.</param>
		///// <returns>The values associated with specified keys.</returns>
		IList<T> Get<T>(IList<string> keys) where T : class;

		/// <summary>
		///     Adds the specified key and object to the cache.
		/// </summary>
		/// <param name="key">key</param>
		/// <param name="data">Data</param>
		/// <param name="cacheTime">Cache time</param>
		void Set<T>(string key, T data, TimeSpan? cacheTime = null) where T : class;

		/// <summary>
		///     Adds the specified keys and objects to the cache.
		/// </summary>
		/// <param name="keyData"></param>
		void Set<T>(IList<Tuple<string, T>> keyData) where T : class;

		/// <summary>
		///     Gets a value indicating whether the value associated with the specified key is cached
		/// </summary>
		/// <param name="key">key</param>
		/// <returns>Result</returns>
		bool IsSet(string key);

		/// <summary>
		///     Removes the value with the specified key from the cache
		/// </summary>
		/// <param name="key">key</param>
		void Remove(string key);

		/// <summary>
		///     Removes the values with the specified keys from the cache
		/// </summary>
		/// <param name="keys">keys</param>
		void RemoveAll(IList<string> keys);

		/// <summary>
		///     Removes items by pattern
		/// </summary>
		/// <param name="pattern">pattern</param>
		void RemoveByPattern(string pattern);

		/// <summary>
		///     Clear all cache data
		/// </summary>
		void Clear();

		Dictionary<string, object> GetAll();

		bool IsUp();

		#endregion

		#region Async

		/// <summary>
		///     Gets or sets the value associated with the specified key.
		/// </summary>
		/// <typeparam name="T">Type</typeparam>
		/// <param name="key">The key of the value to get.</param>
		/// <returns>The value associated with the specified key.</returns>
		Task<T> GetAsync<T>(string key) where T : class;

		/// <summary>
		///     Adds the specified key and object to the cache.
		/// </summary>
		/// <param name="key">key</param>
		/// <param name="data">Data</param>
		/// <param name="cacheTime">Cache time</param>
		Task SetAsync<T>(string key, T data, TimeSpan? cacheTime = null) where T : class;

		/// <summary>
		///     Gets a value indicating whether the value associated with the specified key is cached
		/// </summary>
		/// <param name="key">key</param>
		/// <returns>Result</returns>
		Task<bool> IsSetAsync(string key);

		/// <summary>
		///     Removes the value with the specified key from the cache
		/// </summary>
		/// <param name="key">/key</param>
		Task RemoveAsync(string key);

		/// <summary>
		///     Removes items by pattern
		/// </summary>
		/// <param name="pattern">pattern</param>
		Task RemoveByPatternAsync(string pattern);

		/// <summary>
		///     Clear all cache data
		/// </summary>
		Task ClearAsync();

		Task<Dictionary<string, object>> GetAllAsync();

		Task<bool> IsUpAsync();

		#endregion
	}
}