using System.Collections.Generic;
using System.Threading.Tasks;
using CnapLvivBot.DAL.Entities;

namespace CnapLvivBot.DAL.Infrastructure
{
	public interface IRepository<T> where T : BaseEntity
	{
		#region Sync

		bool Any(int id);
		T Get(int id);
		IList<T> GetAll();
		IList<T> Get(IList<int> ids);
		void Add(T entity);
		void Add(IList<T> entities);
		void Update(T entity);
		void Remove(int id);
		int Count();

		#endregion

		#region Async

		Task<bool> AnyAsync(int id);
		Task<T> GetAsync(int id);
		Task<IList<T>> GetAllAsync();
		Task<List<T>> GetAsync(IList<int> ids);
		Task AddAsync(T entity);
		Task AddAsync(IList<T> entities);
		Task UpdateAsync(T entity);
		Task RemoveAsync(int id);
		Task<int> CountAsync();

		#endregion
	}
}