using Common.Resources;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Common.Model
{
    public abstract class BaseModel
    {
        // Default [Column("id")], override ở child class nếu cần (ví dụ: customer_id, sale_order_id)

        [Column("id")]
        [JsonPropertyName("id")]
        public virtual Guid Id { get; set; }

        [Column("created_by")]
        [JsonPropertyName("createdBy")]
        public string? CreatedBy { get; set; }

        [Column("created_date")]
        [JsonPropertyName("createdDate")]
        public DateTime? CreatedDate { get; set; }

        [Column("modified_by")]
        [JsonPropertyName("modifiedBy")]
        public string? ModifiedBy { get; set; }

        [Column("modified_date")]
        [JsonPropertyName("modifiedDate")]
        public DateTime? ModifiedDate { get; set; }

        [NotMapped]
        [JsonPropertyName("state")]
        public Status State { get; set; } = Status.None;
    }


}
