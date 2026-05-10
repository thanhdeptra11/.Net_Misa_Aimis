using Common.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Common.Model
{
    public class SalaryComposition : BaseModel
    {
        [Column("CompositionId")]
        public override Guid Id { get; set; }

        [Column("OrganizationId")]
        [JsonPropertyName("organizationId")]
        public Guid OrganizationId { get; set; }

        [Column("SystemCompositionId")]
        [JsonPropertyName("systemCompositionId")]
        public Guid? SystemCompositionId { get; set; }

        [Column("CompositionCode")]
        [JsonPropertyName("compositionCode")]
        [Unique]
        [Required]
        public string CompositionCode { get; set; }

        [Column("CompositionName")]
        [JsonPropertyName("compositionName")]
        [Required]
        public string CompositionName { get; set; }

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

        [Column("CreationSource")]
        [JsonPropertyName("creationSource")]
        public string? CreationSource { get; set; }

        [Column("Status")]
        [JsonPropertyName("status")]
        public bool Status { get; set; } = true;

        [Column("CreatedDate")]
        [JsonPropertyName("createdDate")]
        public DateTime CreatedDate { get; set; }

        [Column("CreatedBy")]
        [JsonPropertyName("createdBy")]
        public string? CreatedBy { get; set; }

        [Column("ModifiedDate")]
        [JsonPropertyName("modifiedDate")]
        public DateTime? ModifiedDate { get; set; }

        [Column("ModifiedBy")]
        [JsonPropertyName("modifiedBy")]
        public string? ModifiedBy { get; set; }
    }
}
