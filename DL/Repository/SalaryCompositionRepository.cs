using Common.DTO;
using Common.Model;
using Dapper;
using DL.Base;
using DL.Interface;
using System;
using System.Text.Json;
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
            sc.organization_id AS OrganizationId,
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
            sc.organization_name AS OrganizationName";

        private const string _baseJoinSql = "FROM view_salary_composition_after_join sc";
        //Override câu lệnh filter với cây đệ quy cho tổ chức
        protected override string BuildFilterCondition(
             List<FilterCondition> filters,
             DynamicParameters parameters)
        {
            if (filters == null || !filters.Any())
                return "";

            var conditions = new List<string>();
            var remainingFilters = new List<FilterCondition>();
            var rootOrganizationIds = new List<Guid>();

            foreach (var filter in filters)
            {
                var isOrganizationFilter =
                    string.Equals(filter.Property, "organizationId", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(filter.Property, "organization_id", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(filter.Property, "sc.organization_id", StringComparison.OrdinalIgnoreCase);

                if (!isOrganizationFilter)
                {
                    remainingFilters.Add(filter);
                    continue;
                }

                if (filter.Value is JsonElement jsonElement)
                {
                    if (Guid.TryParse(jsonElement.ToString(), out var organizationId))
                    {
                        rootOrganizationIds.Add(organizationId);
                    }
                }
                else if (filter.Value != null && Guid.TryParse(filter.Value.ToString(), out var organizationId))
                {
                    rootOrganizationIds.Add(organizationId);
                }

            }

            rootOrganizationIds = rootOrganizationIds
                .Distinct()
                .ToList();

            if (rootOrganizationIds.Any())
            {
                parameters.Add("FilterOrganizationIds", rootOrganizationIds);

                conditions.Add(@"
            sc.organization_id IN (
                WITH RECURSIVE org_tree AS (
                    SELECT organization_id
                    FROM pa_organization
                    WHERE organization_id IN @FilterOrganizationIds

                    UNION ALL

                    SELECT child.organization_id
                    FROM pa_organization child
                    INNER JOIN org_tree parent
                        ON child.parent_id = parent.organization_id
                )
                SELECT organization_id FROM org_tree
            )");
            }

            var baseCondition = base.BuildFilterCondition(remainingFilters, parameters);

            if (!string.IsNullOrWhiteSpace(baseCondition))
            {
                conditions.Add(baseCondition);
            }

            return string.Join(" AND ", conditions);
        }

        public async Task<PagingResponse<SalaryCompositionDto>> GetPagingDtoAsync(PagingRequest request)
        {
            return await GetPagingCustomAsync<SalaryCompositionDto>(
                request,
                selectClause: _selectWithMapColumns,
                fromAndJoinClause: _baseJoinSql,
                tableAlias: "sc",
                orderByClause: "sc.modified_date DESC"
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
