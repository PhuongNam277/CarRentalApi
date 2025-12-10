using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewCarRental.Domain.Entities;

namespace NewCarRental.Application.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<string> GetRoleNameByIdAsync(int? roleId);
    }
}
