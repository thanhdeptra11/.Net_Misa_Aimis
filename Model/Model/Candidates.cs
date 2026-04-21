using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Common.Model
{
    [Table("candidates")]
    public class Candidates : BaseModel
    {
        [Column("candidate_id")]
        [JsonPropertyName("candidateId")]
        public override Guid Id { get; set; }

        [Column("candidate_name")]
        [JsonPropertyName("candidateName")]
        public string? CandidateName { get; set; }

        [Column("candidate_dob")]
        [JsonPropertyName("candidateDob")]
        public DateTime? CandidateDob { get; set; }

        [Column("candidate_gender")]
        [JsonPropertyName("candidateGender")]
        public string? CandidateGender { get; set; }

        [Column("candidate_region")]
        [JsonPropertyName("candidateRegion")]
        public string? CandidateRegion { get; set; }

        [Column("candidate_phone_number")]
        [JsonPropertyName("candidatePhoneNumber")]
        public string? CandidatePhoneNumber { get; set; }
        [Column("candidate_email")]
        [JsonPropertyName("candidateEmail")]
        public string? CandidateEmail { get; set; }

        [Column("candidate_country")]
        [JsonPropertyName("candidateCountry")]
        public int? CandidateCountry { get; set; }

        [Column("candidate_province")]
        [JsonPropertyName("candidateProvince")]
        public int? CandidateProvince { get; set; }

        [Column("candidate_ward")]
        [JsonPropertyName("candidateWard")]
        public int? CandidateWard { get; set; }

        [Column("candidate_address_detail")]
        [JsonPropertyName("candidateAddressDetail")]
        public string? CandidateAddressDetail { get; set; }

        // Bảng không có cột lưu thời gian/người tạọ -> bỏ qua mapping tránh lỗi SQL
        [NotMapped]
        public override DateTime CreatedDate { get; set; }

        [NotMapped]
        public override string? CreatedBy { get; set; }

        [NotMapped]
        public override DateTime? ModifiedDate { get; set; }

        [NotMapped]
        public override string? ModifiedBy { get; set; }
    }
}
