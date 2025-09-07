using Polisher.Domain.Repositories;
using Polisher.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


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

    public async Task<List<Polisher.Domain.Entities.Polisher>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Polishers
            .AsNoTracking()
            .Where(p => p.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task<Polisher.Domain.Entities.Polisher> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Polishers
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id && p.IsActive, cancellationToken);
    }

    public async Task UpdateAsync(Polisher.Domain.Entities.Polisher polisher, CancellationToken cancellationToken)
    {
        _context.Polishers.Update(polisher);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var polisher = await _context.Polishers.FindAsync(new object[] { id }, cancellationToken);
        if (polisher != null)
        {
            polisher.IsActive = false; // Soft delete
            _context.Polishers.Update(polisher);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
