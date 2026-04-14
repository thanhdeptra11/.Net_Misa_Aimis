using Common.Model;
using DL.Base;
using DL.Interface;

namespace DL.Repository
{
    public class SaleOrderRepository : BaseDL<SaleOrder>, ISaleOrderRepository
    {
        protected override string TableName => "sale_order";

        protected override string IdColumnName => "sale_order_id";

        public SaleOrderRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }

        // K? th?a t?t c? CRUD operations t? BaseDL<SaleOrder>
        // [Column] attributes s? t? ??ng map property sang column name
    }
}
