using BL.Interface;
using Common.DTO;
using Common.Model;

namespace web_06.Controllers
{
    public class GridConfigController : BaseController<GridConfig, GridConfigCreateDto, GridConfigUpdateDto>
    {
        public GridConfigController(IBaseBL<GridConfig> baseBL) : base(baseBL)
        {
        }
    }
}
