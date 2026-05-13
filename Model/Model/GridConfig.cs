using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Common.Model
{
    public class GridConfig : BaseModel
    {
        [Column("grid_config_id")]
        public override Guid Id { get; set; }

        [Column("orgnization_id")]
        [JsonPropertyName("organizationId")]
        public Guid? OrganizationId { get; set; }

        [Column("user_id")]
        [JsonPropertyName("userId")]
        public Guid? UserId { get; set; }

        [Column("grid_id")]
        [JsonPropertyName("gridId")]
        public string GridId { get; set; } = string.Empty;

        [Column("column_field")]
        [JsonPropertyName("columnField")]
        public string ColumnField { get; set; } = string.Empty;

        [Column("is_visible")]
        [JsonPropertyName("isVisible")]
        public bool IsVisible { get; set; } = true;

        [Column("is_pinned")]
        [JsonPropertyName("isPinned")]
        public bool IsPinned { get; set; } = false;

        [Column("pin_postion")]
        [JsonPropertyName("pinPosition")]
        public string? PinPosition { get; set; }

        [Column("column_order")]
        [JsonPropertyName("columnOrder")]
        public int ColumnOrder { get; set; }

        [Column("column_width")]
        [JsonPropertyName("columnWidth")]
        public int? ColumnWidth { get; set; }
    }
}
