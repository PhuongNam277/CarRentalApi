using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCarRental.Application.Authentication.Common
{
    public record AuthenticationResult (
        string AccessToken,
        string RefreshToken
    );
}
