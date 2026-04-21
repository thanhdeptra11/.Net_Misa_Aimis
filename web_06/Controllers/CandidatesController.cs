using BL.Interface;
using Common.Model;
using Microsoft.AspNetCore.Mvc;

namespace web_06.Controllers
{
    public class CandidatesController : BaseController<Candidates>
    {
        public CandidatesController(ICandidatesBL candidatesBL) : base(candidatesBL)
        {
        } // Kế thừa route api/v1/[controller] từ BaseController
    }
}
