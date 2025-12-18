using MediatR;
using NewCarRental.Application.Authentication.Common;
using NewCarRental.Application.Interfaces.Authentication;
using NewCarRental.Application.Interfaces.Repositories;
using NewCarRental.Domain.Entities;

namespace NewCarRental.Application.Authentication.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthenticationResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public LoginCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator,
            IPasswordHasher passwordHasher, IRoleRepository roleRepository, IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _passwordHasher = passwordHasher;
            _roleRepository = roleRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<AuthenticationResult> Handle(LoginCommand request, CancellationToken cancellationToken)
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

            var refreshToken = new Domain.Entities.RefreshToken
            {
                JwtId = Guid.NewGuid().ToString(),
                Token = _jwtTokenGenerator.GenerateRefreshToken(),
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(1),
                Used = false,
                Invalidated = false,
                UserId = user.UserId
            };

            await _refreshTokenRepository.AddRefreshTokenAsync(refreshToken);

            return new AuthenticationResult(token, refreshToken.Token);
        }
    }
}
