using BL.Interface;
using Common.Model;

namespace web_06.Controllers
{
    public class EmployeesController : BasesController<Employees>
    {
        public EmployeesController(IBaseBL<Employees> baseBL) : base(baseBL)
        {
        }
    }
}
