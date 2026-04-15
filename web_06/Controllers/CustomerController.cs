using BL.Interface;
using Common.Model;

namespace web_06.Controllers
{
    public class CustomerController : BaseController<Customer>
    {
        public CustomerController(ICustomerBL customerBL) : base(customerBL)
        {
        }
    }
}
