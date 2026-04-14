using BL.Interface;
using Common.Model;

namespace web_06.Controllers
{
    public class CustomerController : BasesController<Customer>
    {
        public CustomerController(ICustomerBL customerBL) : base(customerBL)
        {
        }
    }
}
