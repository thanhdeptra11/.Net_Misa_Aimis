using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Common.Model
{
    public class Organization : BaseModel
    {
        [Column("orgnization_id")]
        public override Guid Id { get; set; }

        [Column("parent_id")]
        [JsonPropertyName("parentId")]
        public Guid? ParentId { get; set; }

        [Column("orgnization_name")]
        [JsonPropertyName("organizationName")]
        public string OrganizationName { get; set; } = string.Empty;
    }
}
