using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewCarRental.Domain.Entities;

namespace NewCarRental.Application.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user, string roleName);
        string GenerateRefreshToken();
    }
}
