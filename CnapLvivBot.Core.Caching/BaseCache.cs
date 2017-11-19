using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CnapLvivBot.Core.Caching
{
	[Serializable]
	public abstract class BaseCache<T, TId> where T : class
	{
		private readonly TimeSpan? _defaultExpiration;
		protected readonly ICacheKeyGenerator CacheKeyGenerator;
		protected readonly ICacheManager CacheManager;

		public BaseCache(ICacheManager cacheManager, ICacheKeyGenerator cacheKeyGenerator, TimeSpan? defaultExpiration = null)
		{
			CacheManager = cacheManager;
			CacheKeyGenerator = cacheKeyGenerator;
			_defaultExpiration = defaultExpiration;
		}

		protected abstract string Key(TId id);

		private string KeyInternal(TId id)
		{
			var key = Key(id);
			return CacheKeyGenerator.Generate<T>(key);
		}

		public void Add(TId id, T item)
		{
			CacheManager.Set(KeyInternal(id), item, _defaultExpiration);
		}

		public void AddAll(IDictionary<TId, T> keyDataTuples)
		{
			var tuplesList = keyDataTuples
				.Select(keyDataTuple => new Tuple<string, T>(KeyInternal(keyDataTuple.Key), keyDataTuple.Value)).ToList();
			CacheManager.Set(tuplesList);
		}

		public Task AddAsync(TId id, T item)
		{
			return CacheManager.SetAsync(KeyInternal(id), item, _defaultExpiration);
		}

		public IList<T> GetAll()
		{
			return CacheManager.GetAll<T>(CacheKeyGenerator.Generate<T>(typeof(T).Name));
		}

		protected virtual T Prepare(T obj)
		{
			return obj;
		}

		public T Get(TId id)
		{
			return Prepare(CacheManager.Get<T>(KeyInternal(id)));
		}

		public IList<T> Get(IList<TId> ids)
		{
			return CacheManager.Get<T>(ids.Select(KeyInternal).ToList());
		}

		public void RemoveAll(IList<TId> ids)
		{
			CacheManager.RemoveAll(ids.Select(KeyInternal).ToList());
		}

		public async Task<T> GetAsync(TId id)
		{
			return Prepare(await CacheManager.GetAsync<T>(KeyInternal(id)));
		}

		public T Get(TId id, Func<T> acuire)
		{
			return Prepare(CacheManager.Get(KeyInternal(id), acuire, _defaultExpiration));
		}

		public async Task<T> GetAsync(TId id, Func<Task<T>> acuire)
		{
			return Prepare(await CacheManager.GetAsync(KeyInternal(id), acuire, _defaultExpiration));
		}

		public void Remove(TId id)
		{
			CacheManager.Remove(KeyInternal(id));
		}

		public Task RemoveAsync(TId id)
		{
			return CacheManager.RemoveAsync(KeyInternal(id));
		}
	}
}