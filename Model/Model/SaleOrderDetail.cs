using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Common.Model
{
    /// <summary>
    /// Đại diện cho chi tiết một dòng hàng trong đơn hàng bán
    /// </summary>
    public class SaleOrderDetail : BaseModel
    {
        // Ghi đè Id và đổi tên cột thành customer_id
        [Column("sale_order_detail_id")]
        [JsonPropertyName("id")]
        public override Guid Id
        {
            get => base.Id;
            set => base.Id = value;
        }

        [Column("sale_order_id")]
        [JsonPropertyName("saleOrderId")]
        public Guid SaleOrderId { get; set; }

        [Column("item_name")]
        [JsonPropertyName("itemName")]
        public string? ItemName { get; set; }

        [Column("quantity")]
        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }

        [Column("unit_price")]
        [JsonPropertyName("unitPrice")]
        public decimal UnitPrice { get; set; }

        [Column("amount")]
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        // Kế thừa từ BaseModel:
        // - Id (Column: sale_order_detail_id)
        // - CreatedDate (Column: create_date)
        // - CreatedBy (Column: create_by)
        // - ModifiedDate (Column: modified_date)
        // - ModifiedBy (Column: modified_by)
    }
}
