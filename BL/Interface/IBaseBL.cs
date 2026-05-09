using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common.Model;

namespace BL.Interface
{
    public interface IBaseBL<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<PagingResponse<TEntity>> GetPagingAsync(PagingRequest request);
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<int> AddAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> DeleteAsync(Guid id);
        Task<int> DeleteMultipleAsync(IEnumerable<Guid> ids);
        Task<int> SaveDataAsync(List<TEntity> entities);
    }
}
