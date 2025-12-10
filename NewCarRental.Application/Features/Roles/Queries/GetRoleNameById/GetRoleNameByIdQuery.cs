using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace NewCarRental.Application.Features.Roles.Queries.GetRoleNameById
{
    public class GetRoleNameByIdQuery : IRequest<string>
    {
        public int RoleId { get; set; }
    }
}
