using Common.Model;
using DL.Base;
using DL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Repository
{
    public class EmployeesRepository : BaseDL<Employees>, IEmployeesRepository
    {
        protected override string TableName => "employees";
        protected override string IdColumnName => "employee_id";
        protected override string[] SearchColumns => ["employee_name", "modified_by"];
        public EmployeesRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
        

       
    }
}
