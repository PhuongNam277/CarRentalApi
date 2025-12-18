using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCarRental.Application.Wrappers
{
    public class ErrorResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new List<string>(); // Dành cho lỗi validation nhiều dòng
        public ErrorResponse(string message, List<string>? errors = null)
        {
            Message = message;
            Errors = errors ?? new List<string>();
        }
    }
}
