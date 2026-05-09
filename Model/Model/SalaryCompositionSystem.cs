using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Common.Model
{
    public class SalaryCompositionSystem : BaseModel
    {
        [Column("SystemCompositionId")]
        public override Guid Id { get; set; }

        [Column("CompositionCode")]
        [JsonPropertyName("compositionCode")]
        public string CompositionCode { get; set; } = string.Empty;

        [Column("CompositionName")]
        [JsonPropertyName("compositionName")]
        public string CompositionName { get; set; } = string.Empty;

        [Column("CompositionType")]
        [JsonPropertyName("compositionType")]
        public string? CompositionType { get; set; }

        [Column("Property")]
        [JsonPropertyName("property")]
        public string? Property { get; set; }

        [Column("TaxableType")]
        [JsonPropertyName("taxableType")]
        public string? TaxableType { get; set; }

        [Column("TaxDeductionType")]
        [JsonPropertyName("taxDeductionType")]
        public string? TaxDeductionType { get; set; }

        [Column("Norm")]
        [JsonPropertyName("norm")]
        public string? Norm { get; set; }

        [Column("ValueType")]
        [JsonPropertyName("valueType")]
        public string? ValueType { get; set; }

        [Column("ValueExpression")]
        [JsonPropertyName("valueExpression")]
        public string? ValueExpression { get; set; }

        [Column("Description")]
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [Column("ShowOnPayslip")]
        [JsonPropertyName("showOnPayslip")]
        public string? ShowOnPayslip { get; set; }

        [Column("Status")]
        [JsonPropertyName("status")]
        public bool Status { get; set; } = true;

        [Column("CreatedDate")]
        [JsonPropertyName("createdDate")]
        public override DateTime CreatedDate { get; set; }

        [Column("ModifiedDate")]
        [JsonPropertyName("modifiedDate")]
        public override DateTime? ModifiedDate { get; set; }
    }
}
