using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Common.Model
{
    public class GridConfig : BaseModel
    {
        [Column("GridConfigId")]
        public override Guid Id { get; set; }

        [Column("OrganizationId")]
        [JsonPropertyName("organizationId")]
        public Guid? OrganizationId { get; set; }

        [Column("UserId")]
        [JsonPropertyName("userId")]
        public Guid? UserId { get; set; }

        [Column("GridId")]
        [JsonPropertyName("gridId")]
        public string GridId { get; set; } = string.Empty;

        [Column("ColumnField")]
        [JsonPropertyName("columnField")]
        public string ColumnField { get; set; } = string.Empty;

        [Column("IsVisible")]
        [JsonPropertyName("isVisible")]
        public bool IsVisible { get; set; } = true;

        [Column("IsPinned")]
        [JsonPropertyName("isPinned")]
        public bool IsPinned { get; set; } = false;

        [Column("PinPosition")]
        [JsonPropertyName("pinPosition")]
        public string? PinPosition { get; set; }

        [Column("ColumnOrder")]
        [JsonPropertyName("columnOrder")]
        public int ColumnOrder { get; set; }

        [Column("ColumnWidth")]
        [JsonPropertyName("columnWidth")]
        public int? ColumnWidth { get; set; }

        [Column("CreatedDate")]
        [JsonPropertyName("createdDate")]
        public override DateTime CreatedDate { get; set; }

        [Column("ModifiedDate")]
        [JsonPropertyName("modifiedDate")]
        public override DateTime? ModifiedDate { get; set; }
    }
}
