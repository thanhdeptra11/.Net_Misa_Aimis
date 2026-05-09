using System;

namespace Common.DTO
{
    public class SalaryCompositionCreateDto
    {
        public Guid OrganizationId { get; set; }
        public Guid? SystemCompositionId { get; set; }
        public string CompositionCode { get; set; } = string.Empty;
        public string CompositionName { get; set; } = string.Empty;
        public string? CompositionType { get; set; }
        public string? Property { get; set; }
        public string? TaxableType { get; set; }
        public string? TaxDeductionType { get; set; }
        public string? Norm { get; set; }
        public string? ValueType { get; set; }
        public string? ValueExpression { get; set; }
        public string? Description { get; set; }
        public string? ShowOnPayslip { get; set; }
        public string? CreationSource { get; set; }
        public bool Status { get; set; } = true;
    }

    public class SalaryCompositionUpdateDto : SalaryCompositionCreateDto
    {
        public Guid Id { get; set; }
    }
}
