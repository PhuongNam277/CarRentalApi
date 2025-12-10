using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using NewCarRental.Application.Dtos.Users;

namespace NewCarRental.Application.Features.Users.Queries.GetAllUser
{
    public class GetAllUsersQuery : IRequest<List<UserDetailDto>>
    {

    }
}
