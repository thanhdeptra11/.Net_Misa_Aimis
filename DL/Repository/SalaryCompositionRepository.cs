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
        protected override string IdColumnName => "CompositionId";
        protected override string[] SearchColumns => new string[] { "CompositionCode", "CompositionName" };

        public SalaryCompositionRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        private const string _selectWithMapColumns = @"
            sc.CompositionId AS Id,
            sc.OrganizationId AS OrganizationId,
            sc.SystemCompositionId AS SystemCompositionId,
            sc.CompositionCode AS CompositionCode,
            sc.CompositionName AS CompositionName,
            sc.CompositionType AS CompositionType,
            sc.Property AS Property,
            sc.TaxableType AS TaxableType,
            sc.TaxDeductionType AS TaxDeductionType,
            sc.Norm AS Norm,
            sc.ValueType AS ValueType,
            sc.ValueExpression AS ValueExpression,
            sc.Description AS Description,
            sc.ShowOnPayslip AS ShowOnPayslip,
            sc.CreationSource AS CreationSource,
            sc.Status AS Status,
            sc.CreatedDate AS CreatedDate,
            sc.CreatedBy AS CreatedBy,
            sc.ModifiedDate AS ModifiedDate,
            sc.ModifiedBy AS ModifiedBy,
            sc.OrganizationName AS OrganizationName";

        private const string _baseJoinSql = "FROM view_salary_composition_after_join sc";

        public async Task<PagingResponse<SalaryCompositionDto>> GetPagingDtoAsync(PagingRequest request)
        {
            return await GetPagingCustomAsync<SalaryCompositionDto>(
                request,
                selectClause: _selectWithMapColumns,
                fromAndJoinClause: _baseJoinSql,
                tableAlias: "sc",
                orderByClause: "sc.CreatedDate DESC"
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
