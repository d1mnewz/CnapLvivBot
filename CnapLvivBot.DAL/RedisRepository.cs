using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CnapLvivBot.Core.Caching;
using CnapLvivBot.Core.Caching.Redis;
using CnapLvivBot.DAL.Entities;
using CnapLvivBot.DAL.Infrastructure;

namespace CnapLvivBot.DAL
{
	[Serializable]
	public class RedisRepository<T> : IRepository<T> where T : BaseEntity
	{
		private readonly Cache<T> _cache;
		protected readonly ICacheKeyGenerator CacheKeyGenerator;
		protected readonly ICacheManager CacheManager;

		public RedisRepository(string connectionString)
		{
			CacheManager = new RedisCacheManager(connectionString, errorRetryCount: 3,
				errorRetryInterval: TimeSpan.FromMilliseconds(500));
			;
			CacheKeyGenerator = new VersionedCacheKeyGenerator();
			_cache = new Cache<T>(CacheManager, CacheKeyGenerator);
		}

		public bool Any(int id)
		{
			throw new NotImplementedException();
		}

		public T Get(int id)
		{
			throw new NotImplementedException();
		}

		public IList<T> GetAll()
		{
			throw new NotImplementedException();
		}

		public IList<T> Get(IList<int> ids)
		{
			throw new NotImplementedException();
		}

		public void Add(T entity)
		{
			throw new NotImplementedException();
		}

		public void Add(IList<T> entities)
		{
			throw new NotImplementedException();
		}

		public void Update(T entity)
		{
			throw new NotImplementedException();
		}

		public void Remove(int id)
		{
			throw new NotImplementedException();
		}

		public int Count()
		{
			throw new NotImplementedException();
		}

		public Task<bool> AnyAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<T> GetAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<IList<T>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<List<T>> GetAsync(IList<int> ids)
		{
			throw new NotImplementedException();
		}

		public Task AddAsync(T entity)
		{
			throw new NotImplementedException();
		}

		public Task AddAsync(IList<T> entities)
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(T entity)
		{
			throw new NotImplementedException();
		}

		public Task RemoveAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<int> CountAsync()
		{
			throw new NotImplementedException();
		}
	}
}