using Common.Model;
using DL.Base;
using DL.Interface;

namespace DL.Repository
{
    public class SalaryCompositionSystemRepository : BaseDL<SalaryCompositionSystem>, ISalaryCompositionSystemRepository
    {
        protected override string TableName => "pa_salary_composition_system";
        protected override string IdColumnName => "system_composition_id";
        protected override string[] SearchColumns => new string[] { "composition_code", "composition_name" };

        public SalaryCompositionSystemRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
    }
}
