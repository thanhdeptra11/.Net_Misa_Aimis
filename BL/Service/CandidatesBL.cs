using BL.Base;
using BL.Interface;
using Common.Model;
using DL.Interface;

namespace BL.Service
{
    public class CandidatesBL : BaseBL<Candidates>, ICandidatesBL
    {
        public CandidatesBL(ICandidatesRepository repository) : base(repository)
        {
        }
    }
}
