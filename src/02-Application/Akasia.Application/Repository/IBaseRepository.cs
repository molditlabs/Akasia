using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Akasia.Application.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        // Transaction
        Task CreateAsync(T entity);
        Task<IEnumerable<T>> ReadAllAsync();
        Task<T> ReadByIdAsync(int id);
        Task UpdateAsync(int id, string title, string content);
        Task DeleteAsync(int id);

        // Checks
        Task<bool> IsRecordExistAsync(int id);
    }
}
