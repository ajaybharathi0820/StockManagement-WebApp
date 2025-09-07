using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Domain.Entities;
using Identity.Domain.Repositories;
using Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IdentityDbContext _context;

        public RoleRepository(IdentityDbContext context)
        {
            _context = context;
        }

    public async Task<Role?> GetByIdAsync(Guid id,CancellationToken cancellationToken)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Id == id && r.IsActive);
        }

        public async Task<Role?> GetByNameAsync(string roleName,CancellationToken cancellationToken)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName && r.IsActive);
        }

        public async Task<IReadOnlyList<Role>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Roles.AsNoTracking().Where(r => r.IsActive).ToListAsync();
        }

        public async Task AddAsync(Role role,CancellationToken cancellationToken)
        {
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync(cancellationToken);
        }

         public async Task UpdateAsync(Role role,CancellationToken cancellationToken)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Role role,CancellationToken cancellationToken)
        {
            role.DeleteRole();
            _context.Roles.Update(role);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}