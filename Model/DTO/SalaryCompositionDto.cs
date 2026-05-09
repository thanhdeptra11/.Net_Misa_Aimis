using Common.Model;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.DTO
{
    public class SalaryCompositionDto : SalaryComposition
    {
        [NotMapped]
        [Column("OrganizationName")]
        [JsonPropertyName("organizationName")]
        public string? OrganizationName { get; set; }
    }
}
