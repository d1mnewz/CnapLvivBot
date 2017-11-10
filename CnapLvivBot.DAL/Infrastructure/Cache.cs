using System;
using CnapLvivBot.Core.Caching;
using CnapLvivBot.DAL.Entities;

namespace CnapLvivBot.DAL.Infrastructure
{
	[Serializable]
	public class Cache<T> : BaseCache<T, int> where T : BaseEntity
	{
		public Cache(ICacheManager cacheManager, ICacheKeyGenerator cacheKeyGenerator, TimeSpan? defaultExpiration = null) :
			base(cacheManager, cacheKeyGenerator, defaultExpiration)
		{
		}

		protected override string Key(int id)
		{
			return $"{typeof(T).Name}_{id}";
		}
	}
}