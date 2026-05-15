using System;

namespace Common.DTO
{
    public class SalaryCompositionCreateDto
    {
        public Guid? OrganizationId { get; set; }
        public Guid? SystemCompositionId { get; set; }
        public string CompositionCode { get; set; } = string.Empty;
        public string CompositionName { get; set; } = string.Empty;
        public int? CompositionType { get; set; }
        public int? Property { get; set; }
        public int? TaxableType { get; set; }
        public string? TaxDeductionType { get; set; }
        public string? Norm { get; set; }
        public int? ValueType { get; set; }
        public string? ValueExpression { get; set; }
        public string? Description { get; set; }
        public int? ShowOnPayslip { get; set; }
        public string? CreationSource { get; set; }
        public bool Status { get; set; } = true;
    }

    public class SalaryCompositionUpdateDto : SalaryCompositionCreateDto
    {
        public Guid Id { get; set; }
    }
}
