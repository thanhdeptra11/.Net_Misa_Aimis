using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Reflection;
using Common.Extension;

namespace web_06.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ConstantController : ControllerBase
    {
        /// <summary>
        /// API Lấy danh sách các giá trị của Constant Class dựa vào tên Class
        /// </summary>
        /// <param name="className">Tên của Constant Class</param>
        [HttpGet("{className}")]
        public IActionResult GetConstantValues(string className)
        {
            return Ok(ExtensionUtility.GetConstantValues(className));
        }
    }
}
