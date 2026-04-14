using Common.Model;

namespace BL.Interface
{
    public interface ISaleOrderDetailBL : IBaseBL<SaleOrderDetail>
    {
       
        Task<IEnumerable<SaleOrderDetail>> GetDetailsByOrderIdAsync(Guid saleOrderId);
    }
}
