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
    public class UserRepository : IUserRepository
    {
        private readonly IdentityDbContext _context;

        public UserRepository(IdentityDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(Guid id,CancellationToken cancellationToken) =>
            await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(x => x.Id == id);

        public async Task<User?> GetByUsernameAsync(string username,CancellationToken cancellationToken) =>
            await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(x => x.UserName == username);

        public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken) =>
            await _context.Users.Include(u => u.Role).ToListAsync();

        public async Task AddAsync(User user,CancellationToken cancellationToken)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(User user,CancellationToken cancellationToken)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(User user,CancellationToken cancellationToken)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}