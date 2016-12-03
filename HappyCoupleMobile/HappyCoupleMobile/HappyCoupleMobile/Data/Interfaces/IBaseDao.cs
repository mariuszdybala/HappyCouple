using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HappyCoupleMobile.Data
{
    public interface IBaseDao<T> where T : class, new()
    {
        Task<T> GetFirstAsync();
        Task<T> GetByIdAsync(int id);

        Task<int> InsertAsync(T entity);
        Task<int> InsertAllAsync(IEnumerable<T> entities);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
        Task DeleteAllAsync();

        Task<IList<T>> GetAllAsync();
    }
}