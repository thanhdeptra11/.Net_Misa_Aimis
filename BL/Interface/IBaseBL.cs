using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common.Model;

namespace BL.Interface
{
    public interface IBaseBL<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<PagingResponse<T>> GetPagingAsync(PagingRequest request);
        Task<T?> GetByIdAsync(Guid id);
        Task<int> AddAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(Guid id);
        Task<int> DeleteMultipleAsync(IEnumerable<Guid> ids);
        Task<int> SaveDataAsync(List<T> entities);
    }
}
