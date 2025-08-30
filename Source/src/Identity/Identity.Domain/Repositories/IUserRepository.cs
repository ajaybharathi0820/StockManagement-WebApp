using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Domain.Entities;

namespace Identity.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id,CancellationToken cancellationToken);
        Task<User?> GetByUsernameAsync(string username,CancellationToken cancellationToken);
        Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken);
        Task AddAsync(User user,CancellationToken cancellationToken);
        Task UpdateAsync(User user,CancellationToken cancellationToken);
        Task DeleteAsync(User user,CancellationToken cancellationToken);
    }
}