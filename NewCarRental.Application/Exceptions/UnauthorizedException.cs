using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCarRental.Application.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() : base("Bạn không có quyền truy cập thông tin này.") { }
    }
}
