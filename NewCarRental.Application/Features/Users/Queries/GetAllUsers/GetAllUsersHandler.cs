using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NewCarRental.Application.Dtos.Users;
using NewCarRental.Application.Features.Users.Queries.GetAllUser;
using NewCarRental.Application.Interfaces.Repositories;

namespace NewCarRental.Application.Features.Users.Queries.GetAllUsers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, List<UserDetailDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetAllUsersHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserDetailDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var usersList = await _userRepository.GetAllUsersAsync();
            var data = _mapper.Map<List<UserDetailDto>>(usersList);
            return data;
        }
    }
}
