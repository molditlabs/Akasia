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
        Task ReadAsync();
        Task ReadByIdAsync(int id);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);

        // Checks
        Task<bool> IsRecordExistAsync(int id);
    }
}
