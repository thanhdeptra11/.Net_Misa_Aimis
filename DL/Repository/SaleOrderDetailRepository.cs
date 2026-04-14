using Common.Model;
using Dapper;
using DL.Base;
using DL.Interface;
using System.Reflection;

namespace DL.Repository
{
    public class SaleOrderDetailRepository : BaseDL<SaleOrderDetail>, ISaleOrderDetailRepository
    {
        protected override string TableName => "sale_order_detail";

        protected override string IdColumnName => "sale_order_detail_id";

        public SaleOrderDetailRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }

        /// <summary>
        /// L?y t?t c? chi ti?t ??n hàng theo ID c?a ??n hàng
        /// </summary>
        public async Task<IEnumerable<SaleOrderDetail>> GetDetailsByOrderIdAsync(Guid saleOrderId)
        {
            var properties = typeof(SaleOrderDetail).GetProperties()
                .Where(p => p.CanRead);
            
            var selectColumns = string.Join(", ", properties.Select(p => 
                $"{GetColumnNameStatic(p)} AS {p.Name}"
            ));
            
            string query = $"SELECT {selectColumns} FROM {TableName} WHERE sale_order_id = @SaleOrderId";
            
            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryAsync<SaleOrderDetail>(query, new { SaleOrderId = saleOrderId });
        }
    }
}
