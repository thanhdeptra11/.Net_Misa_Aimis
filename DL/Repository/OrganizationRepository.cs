using Common.Model;
using DL.Base;
using DL.Interface;

namespace DL.Repository
{
    public class OrganizationRepository : BaseDL<Organization>, IOrganizationRepository
    {
        protected override string TableName => "pa_organization";
        protected override string IdColumnName => "OrganizationId";
        protected override string[] SearchColumns => new string[] { "OrganizationName" };

        public OrganizationRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
    }
}
