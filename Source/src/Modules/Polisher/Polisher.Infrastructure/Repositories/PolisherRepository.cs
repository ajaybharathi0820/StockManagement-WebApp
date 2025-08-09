using Polisher.Domain.Repositories;
using Polisher.Infrastructure.Persistence;

namespace Polisher.Infrastructure.Repositories;

public class PolisherRepository : IPolisherRepository
{
    private readonly PolisherDbContext _context;

    public PolisherRepository(PolisherDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Polisher.Domain.Entities.Polisher polisher, CancellationToken cancellationToken)
    {
        await _context.Polishers.AddAsync(polisher, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}