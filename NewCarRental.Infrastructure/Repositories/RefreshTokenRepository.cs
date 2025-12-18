using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NewCarRental.Application.Interfaces.Repositories;
using NewCarRental.Domain.Entities;
using NewCarRental.Infrastructure.Contexts;

namespace NewCarRental.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly CarRentalDbContext _context;
        public RefreshTokenRepository(CarRentalDbContext context)
        {
            _context = context;
        }
        public async Task<RefreshToken?> AddRefreshTokenAsync(RefreshToken refreshToken)
        {
            var result = await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _context.RefreshTokens
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Token == token);
        }

        public async Task RevokeAllTokensByUserIdAsync(int userId)
        {
            var tokens = await _context.RefreshTokens
                .Where(x => x.UserId == userId && !x.Invalidated)
                .ToListAsync();
            foreach (var token in tokens)
            {
                token.Invalidated = true;
            }
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRefreshTokenAsync(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Update(refreshToken);
            await _context.SaveChangesAsync();
        }
    }
}
