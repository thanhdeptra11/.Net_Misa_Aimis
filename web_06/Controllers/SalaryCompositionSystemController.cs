using BL.Interface;
using Common.DTO;
using Common.Model;

namespace web_06.Controllers
{
    public class SalaryCompositionSystemController : BaseController<SalaryCompositionSystem, SalaryCompositionSystemCreateDto, SalaryCompositionSystemUpdateDto>
    {
        public SalaryCompositionSystemController(IBaseBL<SalaryCompositionSystem> baseBL) : base(baseBL)
        {
        }
    }
}
