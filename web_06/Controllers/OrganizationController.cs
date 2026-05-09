using BL.Interface;
using Common.DTO;
using Common.Model;

namespace web_06.Controllers
{
    public class OrganizationController : BaseController<Organization, OrganizationCreateDto, OrganizationUpdateDto>
    {
        public OrganizationController(IBaseBL<Organization> baseBL) : base(baseBL)
        {
        }
    }
}
