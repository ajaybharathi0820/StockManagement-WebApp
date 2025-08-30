using Microsoft.EntityFrameworkCore;

namespace BagType.Infrastructure.Persistence
{
    public class BagTypeDbContext : DbContext
    {
        public BagTypeDbContext(DbContextOptions<BagTypeDbContext> options)
        : base(options)
        {
        }

        public DbSet<BagType.Domain.Entities.BagType> BagTypes => Set<BagType.Domain.Entities.BagType>();
    }
}