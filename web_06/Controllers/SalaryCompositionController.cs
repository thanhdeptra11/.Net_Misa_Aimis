using BL.Interface;
using Common.DTO;
using Common.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace web_06.Controllers
{
    public class SalaryCompositionController : BaseController<SalaryComposition, SalaryCompositionCreateDto, SalaryCompositionUpdateDto>
    {
        private readonly ISalaryCompositionBL _salaryCompositionBL;

        public SalaryCompositionController(ISalaryCompositionBL salaryCompositionBL) : base(salaryCompositionBL)
        {
            _salaryCompositionBL = salaryCompositionBL;
        }

        [HttpPost("filter")]
        public override async Task<IActionResult> Filter([FromBody] PagingRequest request)
        {
            var result = await _salaryCompositionBL.GetPagingDtoAsync(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetById(Guid id)
        {
            var result = await _salaryCompositionBL.GetDtoByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
