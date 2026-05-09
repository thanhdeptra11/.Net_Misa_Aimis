using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Common.Model
{
    public class Organization : BaseModel
    {
        [Column("OrganizationId")]
        public override Guid Id { get; set; }

        [Column("ParentId")]
        [JsonPropertyName("parentId")]
        public Guid? ParentId { get; set; }

        [Column("OrganizationName")]
        [JsonPropertyName("organizationName")]
        public string OrganizationName { get; set; } = string.Empty;

        [Column("CreatedDate")]
        [JsonPropertyName("createdDate")]
        public override DateTime CreatedDate { get; set; }

        [Column("ModifiedDate")]
        [JsonPropertyName("modifiedDate")]
        public override DateTime? ModifiedDate { get; set; }
    }
}
