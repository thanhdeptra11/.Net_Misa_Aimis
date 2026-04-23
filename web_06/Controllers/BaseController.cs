using BL.Interface;
using Common.Model;
using Microsoft.AspNetCore.Mvc;

namespace web_06.Controllers
{
    [ApiController]
    public abstract class BaseController<T, TId> : ControllerBase where T : class
    {
        protected readonly IBaseBL<T, TId> _baseBL;

        public BaseController(IBaseBL<T, TId> baseBL)
        {
            _baseBL = baseBL;
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll()
        {
            var result = await _baseBL.GetAllAsync();
            return Ok(result);
        }

        [HttpPost("filter")]
        public virtual async Task<IActionResult> Filter([FromBody] PagingRequest request)
        {
            var result = await _baseBL.GetPagingAsync(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(TId id)
        {
            var result = await _baseBL.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Add([FromBody] T entity)
        {
            var result = await _baseBL.AddAsync(entity);
            return Ok(new { message = Common.Resources.Messages.AddedSuccessfully, affectedRows = result });
        }

        [HttpPut]
        public virtual async Task<IActionResult> Update([FromBody] T entity)
        {
            var result = await _baseBL.UpdateAsync(entity);
            return Ok(new { message = Common.Resources.Messages.UpdatedSuccessfully, affectedRows = result });
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(TId id)
        {
            var result = await _baseBL.DeleteAsync(id);
            if (result == 0)
                return NotFound();
            return Ok(new { message = Common.Resources.Messages.DeletedSuccessfully, affectedRows = result });
        }

        [HttpPost("delete-multiple")]
        public virtual async Task<IActionResult> DeleteMultiple([FromBody] IEnumerable<TId> ids)
        {
            var result = await _baseBL.DeleteMultipleAsync(ids);
            return Ok(new { message = Common.Resources.Messages.DeletedSuccessfully, affectedRows = result });
        }

        [HttpPost("save-batch")]
        public virtual async Task<IActionResult> SaveBatch([FromBody] List<T> entities)
        {
            try
            {
                if (entities == null || entities.Count == 0)
                    return BadRequest(new { message = "Entities list cannot be null or empty." });

                var result = await _baseBL.SaveDataAsync(entities);
                return Ok(new 
                { 
                    message = "Batch save completed successfully", 
                    totalAffected = result,
                    totalProcessed = entities.Count
                });
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while saving batch data", error = ex.Message });
            }
        }
    }

    [Route("api/v1/[controller]")]
    public abstract class BaseController<T> : BaseController<T, Guid> where T : class
    {
        public BaseController(IBaseBL<T> baseBL) : base(baseBL)
        {
        }
    }
}
