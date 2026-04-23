using Common.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Model
{
    [Table("region2")]
    public class Region : BaseModel<int>
    {
        [Column("RegionID")]
        public override int Id { get; set; }

        [Column("ParentID")]
        public int? ParentId { get; set; }

        [Column("RegionCode")]
        public string? RegionCode { get; set; }

        [Column("RegionName")]
        public string? RegionName { get; set; }

        [Column("RegionNameNotMark")]
        public string? RegionNameNotMark { get; set; }

        [Column("RegionNameAlias")]
        public string? RegionNameAlias { get; set; }

        [Column("RegionLevel")]
        public int? RegionLevel { get; set; }

        [Column("IsActive")]
        public bool? IsActive { get; set; }

        [Column("`Order`")]
        [System.Text.Json.Serialization.JsonPropertyName("order")]
        public int? RegionOrder { get; set; }

        [Column("RegionCodeVHD")]
        public string? RegionCodeVHD { get; set; }

        [Column("IsDelete")]
        public ulong? IsDelete { get; set; }

        // Override lại mapping các trường audit cho đúng với tên cột SQL trong data mẫu của bạn
        [Column("CreateDate")]
        public override DateTime CreatedDate { get; set; }

        [Column("CreateUser")]
        public override string? CreatedBy { get; set; }

        [Column("LastUpdate")]
        public override DateTime? ModifiedDate { get; set; }

        [Column("UpdateUser")]
        public override string? ModifiedBy { get; set; }
    }
}
