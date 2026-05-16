using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class ApiErrorResponse
    {
        public string DevMsg { get; set; } = string.Empty;
        public string UserMsg { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public object? MoreInfor { get; set; }
        public string TraceId { get; set; } = string.Empty;
    }
}
