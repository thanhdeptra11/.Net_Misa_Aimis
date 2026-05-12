using Common.DTO;
using Common.Model;
using Dapper;
using DL.Base;
using DL.Interface;
using System;
using System.Threading.Tasks;

namespace DL.Repository
{
    public class SalaryCompositionRepository : BaseDL<SalaryComposition>, ISalaryCompositionRepository
    {
        protected override string TableName => "pa_salary_composition";
        protected override string IdColumnName => "composition_id";
        protected override string[] SearchColumns => new string[] { "composition_code", "composition_name" };

        public SalaryCompositionRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        private const string _selectWithMapColumns = @"
            sc.composition_id AS Id,
            sc.orgnization_id AS OrganizationId,
            sc.system_composition_id AS SystemCompositionId,
            sc.composition_code AS CompositionCode,
            sc.composition_name AS CompositionName,
            sc.composition_type AS CompositionType,
            sc.property AS Property,
            sc.taxable_type AS TaxableType,
            sc.tax_deduction_type AS TaxDeductionType,
            sc.norm AS Norm,
            sc.value_type AS ValueType,
            sc.value_expression AS ValueExpression,
            sc.description AS Description,
            sc.show_on_payslip AS ShowOnPayslip,
            sc.creation_source AS CreationSource,
            sc.status AS Status,
            sc.created_date AS CreatedDate,
            sc.created_by AS CreatedBy,
            sc.modified_date AS ModifiedDate,
            sc.modified_by AS ModifiedBy,
            sc.orgnization_name AS OrganizationName";

        private const string _baseJoinSql = "FROM view_salary_composition_after_join sc";

        public async Task<PagingResponse<SalaryCompositionDto>> GetPagingDtoAsync(PagingRequest request)
        {
            return await GetPagingCustomAsync<SalaryCompositionDto>(
                request,
                selectClause: _selectWithMapColumns,
                fromAndJoinClause: _baseJoinSql,
                tableAlias: "sc",
                orderByClause: "sc.created_date DESC"
            );
        }

        public async Task<SalaryCompositionDto?> GetDtoByIdAsync(Guid id)
        {
            string query = $@"
                SELECT {_selectWithMapColumns} 
                {_baseJoinSql}
                WHERE sc.CompositionId = @Id";
            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<SalaryCompositionDto>(query, new { Id = id });
        }
    }
}
