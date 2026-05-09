using Common.Model;
using DL.Base;
using DL.Interface;

namespace DL.Repository
{
    public class GridConfigRepository : BaseDL<GridConfig>, IGridConfigRepository
    {
        protected override string TableName => "pa_grid_config";
        protected override string IdColumnName => "GridConfigId";
        protected override string[] SearchColumns => new string[] { "ColumnField", "GridId" };

        public GridConfigRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
    }
}
