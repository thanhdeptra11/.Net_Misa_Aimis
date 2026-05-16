using BL.Interface;
using Common.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace web_06.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public abstract class BaseController<T, TCreateDto, TUpdateDto> : ControllerBase 
        where T : class 
        where TCreateDto : class
        where TUpdateDto : class
    {
        //T: tên của thực thể (Model) là T 
        //TCreateDto: tên của DTO tạo là TCreateDto
        //TUpdateDto: tên của DTO cập nhật là TUpdateDto
        protected readonly IBaseBL<T> _baseBL;

        public BaseController(IBaseBL<T> baseBL)
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
        public virtual async Task<IActionResult> GetById(Guid id)
        {
            var result = await _baseBL.GetByIdAsync(id);
            if (result == null)
                throw new KeyNotFoundException(Common.Resources.Messages.NotFoundRecord);
            return Ok(result);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Add([FromBody] TCreateDto dto)
        {
            T entity;
            
            if (typeof(TCreateDto) == typeof(T))
            {
                entity = (T)(object)dto;
            }
            else
            {
                // Ép kiểu dto về Json
                var json = System.Text.Json.JsonSerializer.Serialize(dto);
                // Ép kiểu Json về T các giá trị không có gán bằng null
                entity = System.Text.Json.JsonSerializer.Deserialize<T>(json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    })!;
            }

            if (entity == null) throw new ArgumentException("Có lỗi xảy ra.");

            var result = await _baseBL.AddAsync(entity);
            return Ok(new { message = Common.Resources.Messages.AddedSuccessfully, affectedRows = result });
        }

        [HttpPut]
        public virtual async Task<IActionResult> Update([FromBody] TUpdateDto dto)
        {
            // Khởi tạo entity
            T entity;

            // Ép kiểu dto về T
            if (typeof(TUpdateDto) == typeof(T))
            {
                // Dùng object để ép kiểu dto về T
                entity = (T)(object)dto;
            }
            else
            {
                var json = System.Text.Json.JsonSerializer.Serialize(dto);
                entity = System.Text.Json.JsonSerializer.Deserialize<T>(json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    })!;
            }

            if (entity == null) throw new ArgumentException("Có lỗi xảy ra.");

            var result = await _baseBL.UpdateAsync(entity);
            return Ok(new { message = Common.Resources.Messages.UpdatedSuccessfully, affectedRows = result });
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            var result = await _baseBL.DeleteAsync(id);
            if (result == 0)
                throw new KeyNotFoundException("Không tìm thấy dữ liệu bản ghi");
            return Ok(new { message = Common.Resources.Messages.DeletedSuccessfully, affectedRows = result });
        }

        [HttpPost("delete-multiple")]
        public virtual async Task<IActionResult> DeleteMultiple([FromBody] IEnumerable<Guid> ids)
        {
            var result = await _baseBL.DeleteMultipleAsync(ids);
            return Ok(new { message = Common.Resources.Messages.DeletedSuccessfully, affectedRows = result });
        }

        [HttpPost("save-batch")]
        public virtual async Task<IActionResult> SaveBatch([FromBody] List<T> entities)
        {
            {
                if (entities == null || entities.Count == 0)
                    throw new ArgumentException("Tham số không null hoặc rỗng.");

                var result = await _baseBL.SaveDataAsync(entities);
                return Ok(new 
                { 
                    message = "Lưu dữ liệu theo lô thành công", 
                    totalAffected = result,
                    totalProcessed = entities.Count
                });
            }
     
        }
    }

    [Route("api/v1/[controller]")]
    [ApiController]
    // Shorthand của BaseController<T, T, T>
    public abstract class BaseController<T> : BaseController<T, T, T> where T : class
    {
        public BaseController(IBaseBL<T> baseBL) : base(baseBL)
        {
        }
    }
}
