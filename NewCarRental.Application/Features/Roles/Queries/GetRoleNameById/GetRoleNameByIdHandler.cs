using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using NewCarRental.Application.Interfaces.Repositories;

namespace NewCarRental.Application.Features.Roles.Queries.GetRoleNameById
{
    public class GetRoleNameByIdHandler : IRequestHandler<GetRoleNameByIdQuery, string>
    {
        public readonly IRoleRepository _roleRepository;
        public GetRoleNameByIdHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public Task<string> Handle(GetRoleNameByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
