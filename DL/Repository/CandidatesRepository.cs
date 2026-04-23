using Common.Model;
using Dapper;
using System.Threading.Tasks;
using System;
using DL.Base;
using DL.Interface;

namespace DL.Repository
{
    public class CandidatesRepository : BaseDL<Candidates>, ICandidatesRepository
    {
        public CandidatesRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        protected override string TableName => "candidates";
        protected override string IdColumnName => "candidate_id"; // Khớp y hệt SQL
        protected override string[] SearchColumns => new string[] { "candidate_name", "candidate_phone_number", "candidate_email" };

        private const string _selectWithMapColumns = @"
            c.candidate_id AS Id, 
            c.candidate_name AS CandidateName, 
            c.candidate_dob AS CandidateDob, 
            c.candidate_gender AS CandidateGender, 
            c.candidate_region AS CandidateRegion, 
            c.candidate_phone_number AS CandidatePhoneNumber, 
            c.candidate_country AS CandidateCountry, 
            c.candidate_province AS CandidateProvince, 
            c.candidate_ward AS CandidateWard, 
            c.candidate_address_detail AS CandidateAddressDetail,
            c.candidate_email AS CandidateEmail,
            r1.RegionName AS CandidateCountryName,
            r2.RegionName AS CandidateProvinceName,
            r3.RegionName AS CandidateWardName";

        private const string _baseJoinSql = @"
            FROM candidates c
            LEFT JOIN region2 r1 ON c.candidate_country = r1.RegionID
            LEFT JOIN region2 r2 ON c.candidate_province = r2.RegionID
            LEFT JOIN region2 r3 ON c.candidate_ward = r3.RegionID";

        public async Task<PagingResponse<Common.DTO.CandidateDto>> GetPagingDtoAsync(PagingRequest request)
        {
            return await GetPagingCustomAsync<Common.DTO.CandidateDto>(
                request,
                selectClause: _selectWithMapColumns,
                fromAndJoinClause: _baseJoinSql,
                tableAlias: "c",
                orderByClause: "c.create_date DESC"
            );
        }

        public async Task<Common.DTO.CandidateDto?> GetDtoByIdAsync(Guid id)
        {
            string query = $@"
                SELECT {_selectWithMapColumns} 
                {_baseJoinSql}
                WHERE c.{IdColumnName} = @Id";
            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Common.DTO.CandidateDto>(query, new { Id = id });
        }
    }
}
