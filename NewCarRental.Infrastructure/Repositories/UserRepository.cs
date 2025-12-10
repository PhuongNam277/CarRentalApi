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
    public class UserRepository : IUserRepository
    {
        private readonly CarRentalDbContext _context;
        
        public UserRepository(CarRentalDbContext context)
        {
            _context = context;
        }
        public async Task<List<User>> GetAllUsersAsync()
        {
            var usersList = await _context.Users.ToListAsync();
            return usersList;
        }

        public async Task<User?> AddUserAsync(User user)
        {
            var result = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return result.Entity;   
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) { return null; }
            return user;
        }
    }
}
