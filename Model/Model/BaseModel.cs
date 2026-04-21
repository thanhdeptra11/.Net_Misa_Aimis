using Common.Resources;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Common.Model
{
    public abstract class BaseModel<TId>
    {
        // Default [Column("id")], override ở child class nếu cần (ví dụ: customer_id, sale_order_id)

        [Column("id")]
        [JsonPropertyName("id")]
        public virtual TId Id { get; set; } = default!;

        [Column("create_date")]
        [JsonPropertyName("createdDate")]
        public virtual DateTime CreatedDate { get; set; }

        [Column("create_by")]
        [JsonPropertyName("createdBy")]
        public virtual string? CreatedBy { get; set; }

        [Column("modified_date")]
        [JsonPropertyName("modifiedDate")]
        public virtual DateTime? ModifiedDate { get; set; }

        [Column("modified_by")]
        [JsonPropertyName("modifiedBy")]
        public virtual string? ModifiedBy { get; set; }

        [NotMapped]
        [JsonPropertyName("state")]
        public Status State { get; set; } = Status.None;
    }

    public abstract class BaseModel : BaseModel<Guid>
    {
    }
}
