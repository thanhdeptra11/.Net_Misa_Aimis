using Common.Extension;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace web_06.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EnumController : ControllerBase
    {
        /// <summary>
        /// API Lấy danh sách các giá trị của Enum dựa vào tên Enum
        /// </summary>
        /// <param name="enumName">Tên của Enum</param>
        [HttpGet("{enumName}")]
        public IActionResult GetEnumValues(string enumName)
        {
            try
            {
                // Tìm kiếm enum type trong các assembly của ứng dụng
                var enumType = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(x => x.GetTypes())
                    .FirstOrDefault(t => t.IsEnum && t.Name.Equals(enumName, StringComparison.OrdinalIgnoreCase));

                if (enumType == null)
                {
                    return NotFound(new { message = $"Enum {enumName} not found." });
                }

                var values = ExtensionUtility.GetEnumValues(enumType);
                return Ok(values);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
