using Microsoft.EntityFrameworkCore;

namespace Polisher.Infrastructure.Persistence;

public class PolisherDbContext : DbContext
{
    public PolisherDbContext(DbContextOptions<PolisherDbContext> options)
    : base(options)
    {
    }

    public DbSet<Polisher.Domain.Entities.Polisher> Polishers => Set<Polisher.Domain.Entities.Polisher>();
}
