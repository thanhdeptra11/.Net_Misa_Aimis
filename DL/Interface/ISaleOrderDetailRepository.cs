using Common.Model;

namespace DL.Interface
{
    public interface ISaleOrderDetailRepository : IBaseDL<SaleOrderDetail>
    {



        Task<IEnumerable<SaleOrderDetail>> GetDetailsByOrderIdAsync(Guid saleOrderId);
    }
}
