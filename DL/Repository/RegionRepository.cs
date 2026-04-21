using DL.Base;
using DL.Interface;
using Model.Model;

namespace DL.Repository
{
    public class RegionRepository : BaseDL<Region, int>, IRegionRepository
    {
        public RegionRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        protected override string TableName => "region2";
        protected override string IdColumnName => "RegionID";
    }
}
