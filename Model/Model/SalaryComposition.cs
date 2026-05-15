using Common.Attributes;
using Common.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Common.Model
{
    public class SalaryComposition : BaseModel
    {
        [Column("composition_id")]
        public override Guid Id { get; set; }

        [Column("organization_id")]
        [JsonPropertyName("organizationId")]
        public Guid? OrganizationId { get; set; }

        [Column("system_composition_id")]
        [JsonPropertyName("systemCompositionId")]
        public Guid? SystemCompositionId { get; set; }

        [Column("composition_code")]
        [JsonPropertyName("compositionCode")]
        [Unique]
        [Required]
        public string CompositionCode { get; set; }

        [Column("composition_name")]
        [JsonPropertyName("compositionName")]
        [Required]
        public string CompositionName { get; set; }

        [Column("composition_type")]
        [JsonPropertyName("compositionType")]
        [EnumType(typeof(CompositionType))]
        public int? CompositionType { get; set; }

        [Column("property")]
        [JsonPropertyName("property")]
        [EnumType(typeof(CompositionProperty))]
        public int? Property { get; set; }

        [Column("taxable_type")]
        [JsonPropertyName("taxableType")]
        [EnumType(typeof(TaxAppliedType))]
        public int? TaxableType { get; set; }

        [Column("tax_deduction_type")]
        [JsonPropertyName("taxDeductionType")]
        public string? TaxDeductionType { get; set; }

        [Column("norm")]
        [JsonPropertyName("norm")]
        public string? Norm { get; set; }

        [Column("value_type")]
        [JsonPropertyName("valueType")]
        [EnumType(typeof(MiValueType))]
        public int? ValueType { get; set; }

        [Column("value_expression")]
        [JsonPropertyName("valueExpression")]
        public string? ValueExpression { get; set; }

        [Column("description")]
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [Column("show_on_payslip")]
        [JsonPropertyName("showOnPayslip")]
        [EnumType(typeof(DisplayPayrollType))]
        public int? ShowOnPayslip { get; set; }

        [Column("creation_source")]
        [JsonPropertyName("creationSource")]
        public string? CreationSource { get; set; }

        [Column("status")]
        [JsonPropertyName("status")]
        public bool Status { get; set; } = true;

        [NotMapped]
        [JsonPropertyName("compositionTypeDescription")]
        public string? CompositionTypeDescription { get; set; }

        [NotMapped]
        [JsonPropertyName("propertyDescription")]
        public string? PropertyDescription { get; set; }

        [NotMapped]
        [JsonPropertyName("taxableTypeDescription")]
        public string? TaxableTypeDescription { get; set; }

        [NotMapped]
        [JsonPropertyName("valueTypeDescription")]
        public string? ValueTypeDescription { get; set; }

        [NotMapped]
        [JsonPropertyName("showOnPayslipDescription")]
        public string? ShowOnPayslipDescription { get; set; }
    }
}
