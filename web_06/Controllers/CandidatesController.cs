using BL.Interface;
using Common.Model;
using Microsoft.AspNetCore.Mvc;

namespace web_06.Controllers
{
    public class CandidatesController : BaseController<Candidates>
    {
        private readonly ICandidatesBL _candidatesBL;

        public CandidatesController(ICandidatesBL candidatesBL) : base(candidatesBL)
        {
            _candidatesBL = candidatesBL;
        }

        [HttpPost("filter")]
        public override async Task<IActionResult> Filter([FromBody] PagingRequest request)
        {
            var result = await _candidatesBL.GetPagingDtoAsync(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetById(Guid id)
        {
            var result = await _candidatesBL.GetDtoByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
