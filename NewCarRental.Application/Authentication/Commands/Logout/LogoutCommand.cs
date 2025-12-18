using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace NewCarRental.Application.Authentication.Commands.Logout
{
    public record LogoutCommand(string RefreshToken) : IRequest<bool>;
}
