using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BagType.Domain.Repositories
{
    public interface IBagTypeRepository
    {
        Task<Entities.BagType?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<List<Entities.BagType>> GetAllAsync(CancellationToken cancellationToken);
        Task AddAsync(Entities.BagType bagType, CancellationToken cancellationToken);
        Task UpdateAsync(Entities.BagType bagType, CancellationToken cancellationToken);
        Task DeleteAsync(Entities.BagType bagType, CancellationToken cancellationToken);
    }
}