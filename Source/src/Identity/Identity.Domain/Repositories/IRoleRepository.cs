using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Domain.Entities;

namespace Identity.Domain.Repositories
{
    public interface IRoleRepository
    {
        Task<Role?> GetByIdAsync(int id,CancellationToken cancellationToken);
        Task<Role?> GetByNameAsync(string roleName,CancellationToken cancellationToken);
        Task<IReadOnlyList<Role>> GetAllAsync(CancellationToken cancellationToken);
        Task AddAsync(Role role,CancellationToken cancellationToken);
        Task UpdateAsync(Role role,CancellationToken cancellationToken);
        Task DeleteAsync(Role role,CancellationToken cancellationToken);
    }
}