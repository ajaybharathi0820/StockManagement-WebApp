using Microsoft.EntityFrameworkCore;

namespace Product.Infrastructure.Persistence
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options)
        {
        }

        public DbSet<Product.Domain.Entities.Product> Products => Set<Product.Domain.Entities.Product>();
    }

}