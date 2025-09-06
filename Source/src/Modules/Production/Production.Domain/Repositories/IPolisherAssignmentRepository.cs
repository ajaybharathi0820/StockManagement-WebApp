using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Production.Domain.Repositories
{
    public interface IPolisherAssignmentRepository
    {
        Task<Entities.PolisherAssignment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<Entities.PolisherAssignment>> SearchAsync(
            ValueObjects.PolisherAssignmentSearch search,
            CancellationToken cancellationToken = default);

        Task AddAsync(Entities.PolisherAssignment assignment, CancellationToken cancellationToken = default);

        Task UpdateAsync(Entities.PolisherAssignment assignment, CancellationToken cancellationToken = default);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}