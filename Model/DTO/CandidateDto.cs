using System;
using System.Text.Json.Serialization;
using Common.Model;

namespace Common.DTO
{
    public class CandidateDto : Candidates
    {
        [JsonPropertyName("candidateCountryName")]
        public string? CandidateCountryName { get; set; }

        [JsonPropertyName("candidateProvinceName")]
        public string? CandidateProvinceName { get; set; }

        [JsonPropertyName("candidateWardName")]
        public string? CandidateWardName { get; set; }
    }
}
