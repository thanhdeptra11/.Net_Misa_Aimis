using BL.Interface;
using Microsoft.AspNetCore.Mvc;
using Model.Model;

namespace web_06.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RegionController : BaseController<Region, int>
    {
        public RegionController(IRegionBL regionBL) : base(regionBL)
        {
        }
    }
}
