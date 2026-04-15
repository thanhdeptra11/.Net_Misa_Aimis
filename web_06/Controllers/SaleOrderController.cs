using BL.Interface;
using Common.Model;

namespace web_06.Controllers
{
    public class SaleOrderController : BaseController<SaleOrder>
    {
        public SaleOrderController(IBaseBL<SaleOrder> saleOrderBL) : base(saleOrderBL)
        {
        }
    }
}
