using System.Net;
using System.Text.Json;

namespace web_06.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            // Handle ValidationException specially: return 400 with message only
            if (exception is System.ComponentModel.DataAnnotations.ValidationException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var validationResponse = new { message = exception.Message };
                return context.Response.WriteAsJsonAsync(validationResponse);
            }

            var statusCode = exception switch
            {
                ArgumentNullException => (int)HttpStatusCode.BadRequest,
                ArgumentException => (int)HttpStatusCode.BadRequest,
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                _ => (int)HttpStatusCode.InternalServerError
            };

            context.Response.StatusCode = statusCode;

            var response = new
            {
                DevMsg = exception.Message,
                UserMsg = "Có lỗi xảy ra vui lòng liên hệ MISA để được hỗ trợ.",
                StatusCode = statusCode,
                MoreInfor = exception.Data,
                TraceId = context.TraceIdentifier
            };

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}