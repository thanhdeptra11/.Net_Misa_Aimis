using Common.DTO;
using Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace web_06.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        // _next: đại diện cho middleware tiếp theo trong pipeline
        // Mỗi middleware nhận _next để có thể gọi middleware kế tiếp
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        // _logger: dùng để ghi log lỗi ra console/file

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //Tự gọi hàm này cho mỗi HTTP request đến
            try
            {
                await _next(context);
                // Gọi middleware tiếp theo trong pipeline
                // Nếu không có lỗi → request đi bình thường đến Controller
            }
            catch (Exception ex)
            // Nếu bất kỳ đâu trong pipeline ném ra Exception → bắt tại đây
            {
                // Ghi log lỗi kèm stack trace
                _logger.LogError(ex, "An unhandled exception occurred");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            //Báo cho client biết có lỗi xảy ra bằng JSON response
            context.Response.ContentType = "application/json";
            //Map các exception với mã lỗi HTTP tương ứng
            var statusCode = exception switch
            {
                ArgumentNullException => (int)HttpStatusCode.BadRequest,
                ArgumentException => (int)HttpStatusCode.BadRequest,
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                BusinessException => (int)HttpStatusCode.BadRequest,
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                _ => (int)HttpStatusCode.InternalServerError
            };
            //Gán mã lỗi vào HTTP Response
            context.Response.StatusCode = statusCode;
            var userMsg = exception switch
            {
                ValidationException => exception.Message,
                KeyNotFoundException => exception.Message,
                BusinessException => exception.Message,
                UnauthorizedAccessException => "Bạn không có quyền thực hiện thao tác này.",
                _ => "Có lỗi xảy ra vui lòng liên hệ MISA để được hỗ trợ."
            };
            var response = new ApiErrorResponse
            {
                DevMsg = exception.Message,
                UserMsg = userMsg,
                StatusCode = statusCode,
                MoreInfor = exception.Data,
                TraceId = context.TraceIdentifier
            };
            // Trả về JSON response chứa thông tin lỗi cho client
            return context.Response.WriteAsJsonAsync(response);
        }
    }
}