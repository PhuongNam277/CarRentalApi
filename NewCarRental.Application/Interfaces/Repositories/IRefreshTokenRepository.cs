using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewCarRental.Domain.Entities;

namespace NewCarRental.Application.Interfaces.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> AddRefreshTokenAsync(RefreshToken refreshToken);
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task RevokeAllTokensByUserIdAsync(int userId);
        Task UpdateRefreshTokenAsync(RefreshToken refreshToken);
    }
}
