namespace DL.Interface
{
    public interface IBaseDL<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Guid id);
        Task<int> AddAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(Guid id);
        Task<int> DeleteMultipleAsync(IEnumerable<Guid> ids);
        Task<bool> CheckDupblicate(string propertyName, object value, Guid? excludeId = null);
    }
}
