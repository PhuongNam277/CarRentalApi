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
        public RoleRepository(CarRentalDbContext context)
        {
            _context = context;
        }

        public async Task<string> GetRoleNameByIdAsync(int? roleId)
        {
            var role = await _context.Roles.FindAsync(roleId);
            return role == null ? throw new InvalidOperationException($"Role with id {roleId} not found.") : role.RoleName;
        }
    }
}
