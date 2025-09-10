using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BagType.Domain.Repositories
{
    public interface IBagTypeRepository
    {
    Task<Entities.BagType?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<Entities.BagType>> GetAllAsync(CancellationToken cancellationToken);
        Task<bool> IsNameExistsAsync(string name, Guid? excludeId = null, CancellationToken cancellationToken = default);
        Task AddAsync(Entities.BagType bagType, CancellationToken cancellationToken);
        Task UpdateAsync(Entities.BagType bagType, CancellationToken cancellationToken);
        Task DeleteAsync(Entities.BagType bagType, CancellationToken cancellationToken);
    }
}