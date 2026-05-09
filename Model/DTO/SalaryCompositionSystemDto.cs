using System;

namespace Common.DTO
{
    public class SalaryCompositionSystemCreateDto
    {
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
        public bool Status { get; set; } = true;
    }

    public class SalaryCompositionSystemUpdateDto : SalaryCompositionSystemCreateDto
    {
        public Guid Id { get; set; }
    }
}
