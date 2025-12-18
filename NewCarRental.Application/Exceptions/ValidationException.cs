using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCarRental.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public List<string> Errors { get; } = new List<string>();
        public ValidationException(List<string> errors)
            : base("One or more validation failures have occurred.")  
        { 
            Errors = errors;
        }
        public ValidationException(string error)
            : base("One or more validation failures have occurred.")
        {
            Errors = new List<string>() { error };
        }
    }
}
