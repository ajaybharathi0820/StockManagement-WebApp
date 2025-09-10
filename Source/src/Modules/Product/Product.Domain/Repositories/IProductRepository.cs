using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Domain.Repositories
{
    public interface IProductRepository
    {
        // Create
        Task AddAsync(Domain.Entities.Product product, CancellationToken cancellationToken = default);

        // Read
        Task<Domain.Entities.Product> GetByCodeAsync(string productCode, CancellationToken cancellationToken = default);
        Task<IEnumerable<Domain.Entities.Product>> GetAllAsync(CancellationToken cancellationToken = default);

        // Validation
        Task<bool> IsNameExistsAsync(string name, Guid? excludeId = null, CancellationToken cancellationToken = default);
        Task<bool> IsCodeExistsAsync(string code, Guid? excludeId = null, CancellationToken cancellationToken = default);

        // Update
        Task UpdateAsync(Domain.Entities.Product product, CancellationToken cancellationToken = default);

        // Delete (soft delete using IsActive flag)
        Task<Domain.Entities.Product> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}