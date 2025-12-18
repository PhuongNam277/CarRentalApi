using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCarRental.Application.Exceptions
{
    public class UnauthenticatedException : Exception
    {
        public UnauthenticatedException() : base("Vui lòng đăng nhập để tiếp tục.") { }
    }
}
