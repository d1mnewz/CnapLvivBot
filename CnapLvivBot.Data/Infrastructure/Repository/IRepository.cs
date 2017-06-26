using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CnapLvivBot.Data.Infrastructure.Repository
{
    public interface IRepository<T, TId> : IDisposable
    {
        Task<bool> AnyAsync(TId id);
        Task<T> GetAsync(TId id);
        Task<IList<T>> GetAllAsync();
        Task<List<T>> GetAsync(IList<TId> ids);
        Task InsertAsync(T entity);
        Task InsertAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task DeleteAsync(TId id);

    }
}