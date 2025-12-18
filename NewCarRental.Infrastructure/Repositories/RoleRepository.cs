using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewCarRental.Application.Interfaces.Repositories;
using NewCarRental.Domain.Entities;
using NewCarRental.Infrastructure.Contexts;

namespace NewCarRental.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly CarRentalDbContext _context;
        private readonly IUserRepository _userRepository;
        public RoleRepository(CarRentalDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<string> GetRoleNameByIdAsync(int? roleId)
        {
            var role = await _context.Roles.FindAsync(roleId);
            return role == null ? throw new InvalidOperationException($"Role with id {roleId} not found.") : role.RoleName;
        }

        public async Task<string> GetRoleNameByUserIdAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) { return null!; }
            var roleName = await GetRoleNameByIdAsync(user.RoleId);
            if (roleName == null) { return null!; }
            return roleName;
        }
    }
}
