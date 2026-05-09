using BL.Base;
using BL.Interface;
using Common.Model;
using DL.Interface;

namespace BL.Service
{
    public class OrganizationBL : BaseBL<Organization>, IOrganizationBL
    {
        public OrganizationBL(IBaseDL<Organization> baseDL) : base(baseDL)
        {
        }
    }
}
