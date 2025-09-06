using Microsoft.EntityFrameworkCore;
using Production.Domain.Repositories;
using Production.Infrastructure.Persistence;

namespace Production.Infrastructure.Repositories
{
    public class PolisherAssignmentRepository : IPolisherAssignmentRepository
    {
        private readonly ProductionDbContext _dbContext;

        public PolisherAssignmentRepository(ProductionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Domain.Entities.PolisherAssignment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.PolisherAssignments
                .Include(pa => pa.Items) // load children
                .FirstOrDefaultAsync(pa => pa.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Domain.Entities.PolisherAssignment>> SearchAsync(
            Domain.ValueObjects.PolisherAssignmentSearch search,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Domain.Entities.PolisherAssignment> query = _dbContext.PolisherAssignments
                .Include(pa => pa.Items);

            if (search.PolisherId.HasValue)
            {
                query = query.Where(pa => pa.PolisherId == search.PolisherId.Value);
            }

            if (search.ProductId.HasValue)
            {
                query = query.Where(pa => pa.Items.Any(i => i.ProductId == search.ProductId.Value));
            }

            if (search.FromDate.HasValue)
            {
                query = query.Where(pa => pa.CreatedDate >= search.FromDate.Value);
            }

            if (search.ToDate.HasValue)
            {
                query = query.Where(pa => pa.CreatedDate <= search.ToDate.Value);
            }

            return await query.ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Domain.Entities.PolisherAssignment assignment, CancellationToken cancellationToken = default)
        {
            await _dbContext.PolisherAssignments.AddAsync(assignment, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Domain.Entities.PolisherAssignment assignment, CancellationToken cancellationToken = default)
        {
            _dbContext.PolisherAssignments.Update(assignment);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var assignment = await _dbContext.PolisherAssignments
                .Include(pa => pa.Items)
                .FirstOrDefaultAsync(pa => pa.Id == id, cancellationToken);

            if (assignment != null)
            {
                _dbContext.PolisherAssignments.Remove(assignment);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
