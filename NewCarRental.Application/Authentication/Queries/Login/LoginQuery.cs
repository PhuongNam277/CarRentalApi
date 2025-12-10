using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace NewCarRental.Application.Authentication.Queries.Login
{
    public record LoginQuery(string email, string password) : IRequest<string>;
}
