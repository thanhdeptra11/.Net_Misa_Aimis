using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Common.Model
{
    public class SalaryCompositionSystem : BaseModel
    {
        [Column("system_composition_id")]
        public override Guid Id { get; set; }

        [Column("composition_code")]
        [JsonPropertyName("compositionCode")]
        public string CompositionCode { get; set; } = string.Empty;

        [Column("composition_name")]
        [JsonPropertyName("compositionName")]
        public string CompositionName { get; set; } = string.Empty;

        [Column("composition_type")]
        [JsonPropertyName("compositionType")]
        public int? CompositionType { get; set; }

        [Column("property")]
        [JsonPropertyName("property")]
        public int? Property { get; set; }

        [Column("taxable_type")]
        [JsonPropertyName("taxableType")]
        public int? TaxableType { get; set; }

        [Column("tax_deduction_type")]
        [JsonPropertyName("taxDeductionType")]
        public string? TaxDeductionType { get; set; }

        [Column("norm")]
        [JsonPropertyName("norm")]
        public string? Norm { get; set; }

        [Column("value_type")]
        [JsonPropertyName("valueType")]
        public int? ValueType { get; set; }

        [Column("value_expression")]
        [JsonPropertyName("valueExpression")]
        public string? ValueExpression { get; set; }

        [Column("description")]
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [Column("show_on_payslip")]
        [JsonPropertyName("showOnPayslip")]
        public int? ShowOnPayslip { get; set; }

        [Column("status")]
        [JsonPropertyName("status")]
        public bool Status { get; set; } = true;

    }
}
