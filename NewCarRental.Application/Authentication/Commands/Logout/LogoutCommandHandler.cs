using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using NewCarRental.Application.Interfaces.Repositories;

namespace NewCarRental.Application.Authentication.Commands.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, bool>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public LogoutCommandHandler(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var storedToken = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken);
            if (storedToken == null || storedToken.Invalidated) return true;

            storedToken.Invalidated = true;

            await _refreshTokenRepository.UpdateRefreshTokenAsync(storedToken);
            return true;
        }
    }
}
