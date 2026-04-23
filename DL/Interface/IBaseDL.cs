using Common.Model;

namespace DL.Interface
{
    public interface IBaseDL<TEntity, TId> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<PagingResponse<TEntity>> GetPagingAsync(PagingRequest request);
        Task<TEntity?> GetByIdAsync(TId id);
        Task<int> AddAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> DeleteAsync(TId id);
        Task<int> DeleteMultipleAsync(IEnumerable<TId> ids);
        Task<bool> CheckDupblicate(string propertyName, object value, object? excludeId = null);
    }

    public interface IBaseDL<T> : IBaseDL<T, Guid> where T : class
    {
    }
}
