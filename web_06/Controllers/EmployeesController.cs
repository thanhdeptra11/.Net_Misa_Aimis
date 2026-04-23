using BL.Interface;
using Common.Model;

namespace web_06.Controllers
{
    public class EmployeesController : BaseController<Employees>
    {
        public EmployeesController(IBaseBL<Employees> baseBL) : base(baseBL)
        {
        }
    }
}
