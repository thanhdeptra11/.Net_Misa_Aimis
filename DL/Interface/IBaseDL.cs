using Common.Model;

namespace DL.Interface
{
    public interface IBaseDL<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<PagingResponse<TEntity>> GetPagingAsync(PagingRequest request);
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<int> AddAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> DeleteAsync(Guid id);
        Task<int> DeleteMultipleAsync(IEnumerable<Guid> ids);
        Task<bool> CheckDuplicate(string propertyName, object value, object? excludeId = null);
    }
}
