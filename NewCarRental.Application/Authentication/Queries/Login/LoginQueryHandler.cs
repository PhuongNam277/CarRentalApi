using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using NewCarRental.Application.Interfaces.Authentication;
using NewCarRental.Application.Interfaces.Repositories;

namespace NewCarRental.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IPasswordHasher _passwordHasher;
        public LoginQueryHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator, IPasswordHasher passwordHasher, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _passwordHasher = passwordHasher;
            _roleRepository = roleRepository;
        }

        public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.email);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var roleName = await _roleRepository.GetRoleNameByIdAsync(user.RoleId);
            if (roleName == null)
            {
                throw new Exception("Cannot find role name");
            }


            if (!_passwordHasher.VerifyPassword(request.password, user.PasswordHash))
            {
                throw new Exception("Password incorrect");
            }

            var token = _jwtTokenGenerator.GenerateToken(user, roleName);
            return token;

        }
    }
}
