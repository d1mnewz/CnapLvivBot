using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CnapLvivBot.Data.Infrastructure.Repository
{
    public interface IRepository<T> : IDisposable
    {
        Task<bool> AnyAsync(string id);
        Task<T> GetAsync(string id);
        Task<IList<T>> GetAllAsync();
        Task<List<T>> GetAsync(IList<string> ids);
        Task InsertAsync(T entity);
        Task InsertAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task DeleteAsync(string id);

    }
}