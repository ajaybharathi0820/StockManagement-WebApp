using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using Product.Domain.Repositories;
using Product.Infrastructure.Persistence;

namespace Product.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;

        public ProductRepository(ProductDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Product.Domain.Entities.Product product, CancellationToken cancellationToken = default)
        {
            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Product.Domain.Entities.Product> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id && p.IsActive, cancellationToken);
        }

        public async Task<Product.Domain.Entities.Product> GetByCodeAsync(string productCode, CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.ProductCode == productCode && p.IsActive, cancellationToken);
        }

        public async Task<IEnumerable<Product.Domain.Entities.Product>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .Where(p => p.IsActive)
                .ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(Product.Domain.Entities.Product product, CancellationToken cancellationToken = default)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync(cancellationToken);
        }
        
        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var product = await _context.Products.FindAsync(new object[] { id }, cancellationToken);

            if (product != null)
            {
                product.IsActive = false; // Soft delete
                _context.Products.Update(product);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

    }
}
