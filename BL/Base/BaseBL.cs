using Common.Attributes;
using Common.Model;
using Common.Resources;
using Dapper;
using DL.Interface;
using System.ComponentModel.DataAnnotations;

namespace BL.Base
{
    public abstract class BaseBL<T> : Interface.IBaseBL<T> where T : BaseModel
    {
        protected IBaseDL<T> _baseDL;

        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _baseDL.GetAllAsync();
        }

        public virtual async Task<PagingResponse<T>> GetPagingAsync(PagingRequest request)
        {
            return await _baseDL.GetPagingAsync(request);
        }

        /// <summary>
        /// Lưu dữ liệu hàng loạt (Insert/Update/Delete) với trạng thái
        /// </summary>
        public async Task<int> SaveDataAsync(List<T> baseModels)
        {
            if (baseModels == null || baseModels.Count == 0)
                throw new ArgumentException("Entities list cannot be null or empty.");

            var rowInserts = baseModels.Where(b => b.State == Status.Insert).ToList();
            var rowUpdates = baseModels.Where(b => b.State == Status.Update).ToList();
            var rowDeletes = baseModels.Where(b => b.State == Status.Delete).ToList();

            int totalAffected = 0;

            // Validate trước khi save
            await BeforeSave(baseModels);

            // Insert
            foreach (var item in rowInserts)
            {
                var result = await AddAsync(item);
                totalAffected += result;
            }

            // Update
            foreach (var item in rowUpdates)
            {
                var result = await UpdateAsync(item);
                totalAffected += result;
            }

            // Delete
            foreach (var item in rowDeletes)
            {
                var result = await DeleteAsync(item.Id);
                totalAffected += result;
            }

            return totalAffected;
        }

        /// <summary>
        /// Hook để validate dữ liệu trước khi save
        /// </summary>
        public virtual async Task BeforeSave(List<T> baseModels)
        {
            foreach (var model in baseModels)
            {
                // Determine isUpdate based on State
                bool isUpdate = model.State == Status.Update;

                // Validate từng entity trước khi save
                var isValid = await ValidateBusinessRulesAsync(model, isUpdate);
                if (!isValid)
                    throw new ValidationException($"Entity {model.GetType().Name} does not satisfy business rules.");
            }

            await Task.CompletedTask;
        }

        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            return await _baseDL.GetByIdAsync(id);
        }

        public virtual async Task<int> AddAsync(T entity)
        {
            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }
            entity.CreatedDate = DateTime.UtcNow;
            entity.CreatedBy = "System";
            entity.ModifiedDate = DateTime.UtcNow;
            entity.ModifiedBy = "System";

            var isValid = await ValidateBusinessRulesAsync(entity, isUpdate: false);
            if (!isValid) throw new Exception("Entity does not satisfy business rules.");
            return await _baseDL.AddAsync(entity);
        }

        public virtual async Task<int> UpdateAsync(T entity)
        {
            entity.ModifiedDate = DateTime.UtcNow;
            entity.ModifiedBy = "System";

            var isValid = await ValidateBusinessRulesAsync(entity, isUpdate: true);
            if (!isValid) throw new Exception("Entity does not satisfy business rules.");
            return await _baseDL.UpdateAsync(entity);
        }

        public virtual async Task<int> DeleteAsync(Guid id)
        {
            return await _baseDL.DeleteAsync(id);
        }

        public virtual async Task<int> DeleteMultipleAsync(IEnumerable<Guid> ids)
        {
            return await _baseDL.DeleteMultipleAsync(ids);
        }
        
        public async Task<bool> ValidateBusinessRulesAsync(T entity, bool isUpdate = false)
        {
            var listProrerty = typeof(T).GetProperties();
            foreach (var property in listProrerty)
            {
                // Kiểm tra required nếu property có attribute [Required]
                var requiredAttribute = property.GetCustomAttributes(typeof(RequiredAttribute), false).FirstOrDefault() as RequiredAttribute;
                if (requiredAttribute != null)
                {
                    var value = property.GetValue(entity);
                    if (value == null || (value is string str && string.IsNullOrWhiteSpace(str)))
                    {
                        var display = property.Name;
                        throw new ValidationException(string.Format(Common.Resources.Messages.FieldIsRequired, display));
                    }
                }
                
                // Kiểm tra unique nếu property có attribute [Unique]
                var uniqueAttribute = property.GetCustomAttributes(typeof(UniqueAttribute), false).FirstOrDefault() as UniqueAttribute;
                if (uniqueAttribute != null)
                {
                    var value = property.GetValue(entity);
                    // Nếu value null, coi như không duplicate
                    if (value != null)
                    {
                        // Chỉ set excludeId khi UPDATE để không loại trừ record hiện tại
                        object? excludeId = null;
                        if (isUpdate)
                        {
                            var idProp = typeof(T).GetProperty("Id");
                            if (idProp != null)
                            {
                                excludeId = idProp.GetValue(entity);
                            }
                        }

                        var isDup = await _baseDL.CheckDuplicate(property.Name, value, excludeId);
                        if (isDup)
                        {
                            var message = uniqueAttribute.Message ?? string.Format(Common.Resources.Messages.AlreadyExists, property.Name);
                            throw new ValidationException(message);
                        }
                    }
                }
            }
            // Mặc định trả về true, các BL cụ thể có thể override để thêm logic validate riêng
            return await Task.FromResult(true);
        }
    }
}
