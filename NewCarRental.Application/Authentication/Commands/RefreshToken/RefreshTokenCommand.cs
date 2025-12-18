using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using NewCarRental.Application.Authentication.Common;

namespace NewCarRental.Application.Authentication.Commands.RefreshToken
{
    public record RefreshTokenCommand(string RefreshToken) : IRequest<AuthenticationResult>;
}
