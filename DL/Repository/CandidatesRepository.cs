using Common.Model;
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
    }
}
