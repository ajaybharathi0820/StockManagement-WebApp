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
            await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(x => x.Id == id && x.IsActive);

        public async Task<User?> GetByUsernameAsync(string username,CancellationToken cancellationToken) =>
            await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(x => x.UserName == username && x.IsActive);

        public async Task<User?> GetByEmailAsync(string email,CancellationToken cancellationToken) =>
            await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(x => x.Email == email && x.IsActive);

        public async Task<bool> IsUsernameExistsAsync(string username, Guid? excludeUserId = null, CancellationToken cancellationToken = default)
        {
            var query = _context.Users.Where(u => u.UserName.ToLower() == username.ToLower() && u.IsActive);
            if (excludeUserId.HasValue)
                query = query.Where(u => u.Id != excludeUserId.Value);
            return await query.AnyAsync(cancellationToken);
        }

        public async Task<bool> IsEmailExistsAsync(string email, Guid? excludeUserId = null, CancellationToken cancellationToken = default)
        {
            var query = _context.Users.Where(u => u.Email.ToLower() == email.ToLower() && u.IsActive);
            if (excludeUserId.HasValue)
                query = query.Where(u => u.Id != excludeUserId.Value);
            return await query.AnyAsync(cancellationToken);
        }

        public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken) =>
            await _context.Users.Include(u => u.Role).Where(u => u.IsActive).ToListAsync();

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
            user.DeleteUser(); // Soft delete
            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}