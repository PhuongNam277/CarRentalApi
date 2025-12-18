using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using NewCarRental.Application.Authentication.Common;
using NewCarRental.Application.Interfaces.Authentication;
using NewCarRental.Application.Interfaces.Repositories;

namespace NewCarRental.Application.Authentication.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthenticationResult>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        public RefreshTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository, 
            IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository, 
            IRoleRepository roleRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<AuthenticationResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var storedToken = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken);

            if (storedToken == null) throw new Exception("Refresh Token not exist");

            if (storedToken.ExpiryDate < DateTime.UtcNow)
            {
                throw new Exception("Refresh token is expired, please login again");
            }

            if (storedToken.Invalidated) throw new Exception("Refresh token has been invalidated");

            if (storedToken.Used)
            {
                // Revoked all tokens
                await _refreshTokenRepository.RevokeAllTokensByUserIdAsync(storedToken.UserId);
                throw new Exception("Token reuse detected. All token will be revoked");
            }

            storedToken.Used = true;
            await _refreshTokenRepository.UpdateRefreshTokenAsync(storedToken);

            // Create new access token
            var user = await _userRepository.GetUserByIdAsync(storedToken.UserId);
            if (user == null) throw new Exception("User not found");
            var roleName = await _roleRepository.GetRoleNameByUserIdAsync(storedToken.UserId);
            if (roleName == null) throw new Exception("Role name not found");

            var newAccessToken = _jwtTokenGenerator.GenerateToken(user, roleName);
            var newRefreshToken = _jwtTokenGenerator.GenerateRefreshToken();

            // Save to db
            var newRefreshTokenEntity = new Domain.Entities.RefreshToken
            {
                JwtId = Guid.NewGuid().ToString(),
                Token = newRefreshToken,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(1),
                Used = false,
                Invalidated = false,
                UserId = user.UserId
            };

            await _refreshTokenRepository.AddRefreshTokenAsync(newRefreshTokenEntity);

            return new AuthenticationResult(newAccessToken, newRefreshToken);
        }
    }
}
