using BL.Base;
using BL.Interface;
using Common.Model;
using DL.Interface;

namespace BL.Service
{
    public class SaleOrderBL : BaseBL<SaleOrder>, ISaleOrderBL
    {
        private readonly ISaleOrderRepository _repository;

        public SaleOrderBL(ISaleOrderRepository repository) : base(repository)
        {
            _repository = repository;
        }

        // Override AddAsync ?? set ID t? ??ng
        public override async Task<int> AddAsync(SaleOrder entity)
        {
            entity.Id = Guid.NewGuid();
            entity.CreatedDate = DateTime.UtcNow;
            return await base.AddAsync(entity);
        }

        // Override UpdateAsync ?? update ModifiedDate
        public override async Task<int> UpdateAsync(SaleOrder entity)
        {
            entity.ModifiedDate = DateTime.UtcNow;
            return await base.UpdateAsync(entity);
        }

        // K? th?a t?t c? CRUD operations khác t? BaseBL<SaleOrder>
    }
}
