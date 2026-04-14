using Common.Model;
using DL.Base;
using DL.Interface;
using System;

namespace DL.Repository
{
    public class CustomerRepository : BaseDL<Customer>, ICustomerRepository
    {
        protected override string TableName => "customer";
        protected override string IdColumnName => "customer_id";
        public CustomerRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }

        // Kế thừa tất cả CRUD operations từ BaseDL<Customer>
        // [Column] attributes sẽ tự động map property sang column name
    }
}
