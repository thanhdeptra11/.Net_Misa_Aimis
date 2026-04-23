using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common.Model;

namespace BL.Interface
{
    public interface IBaseBL<TEntity, TId> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<PagingResponse<TEntity>> GetPagingAsync(PagingRequest request);
        Task<TEntity?> GetByIdAsync(TId id);
        Task<int> AddAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> DeleteAsync(TId id);
        Task<int> DeleteMultipleAsync(IEnumerable<TId> ids);
        Task<int> SaveDataAsync(List<TEntity> entities);
    }

    public interface IBaseBL<T> : IBaseBL<T, Guid> where T : class
    {
    }
}
