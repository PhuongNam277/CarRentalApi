using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using NewCarRental.Application.Authentication.Common;

namespace NewCarRental.Application.Authentication.Commands.Login
{
    public record LoginCommand(string email, string password) : IRequest<AuthenticationResult>;
}
