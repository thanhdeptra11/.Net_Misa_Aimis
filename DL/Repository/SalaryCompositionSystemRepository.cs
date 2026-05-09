using Common.Model;
using DL.Base;
using DL.Interface;

namespace DL.Repository
{
    public class SalaryCompositionSystemRepository : BaseDL<SalaryCompositionSystem>, ISalaryCompositionSystemRepository
    {
        protected override string TableName => "pa_salary_composition_system";
        protected override string IdColumnName => "SystemCompositionId";
        protected override string[] SearchColumns => new string[] { "CompositionCode", "CompositionName" };

        public SalaryCompositionSystemRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
    }
}
