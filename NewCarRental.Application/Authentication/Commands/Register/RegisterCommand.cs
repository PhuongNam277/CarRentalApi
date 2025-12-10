using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace NewCarRental.Application.Authentication.Commands.Register
{
    public record RegisterCommand(
        string FullName,
        string Username,
        string PasswordHash,
        string Email,
        int IsBlocked,
        int RoleId
    ) : IRequest<int>;
}
