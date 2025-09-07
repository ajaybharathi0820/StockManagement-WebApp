using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BagType.Infrastructure.Persistence;
using BagType.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BagType.Infrastructure.Repositories
{
    public class BagTypeRepository : IBagTypeRepository
    {
        private readonly BagTypeDbContext _context;

        public BagTypeRepository(BagTypeDbContext context)
        {
            _context = context;
        }

    public async Task<BagType.Domain.Entities.BagType?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.BagTypes.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id && b.IsActive, cancellationToken);
        }
        public async Task<List<BagType.Domain.Entities.BagType>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.BagTypes.AsNoTracking().Where(b => b.IsActive).ToListAsync(cancellationToken);
        }

        public async Task AddAsync(BagType.Domain.Entities.BagType bagType, CancellationToken cancellationToken)
        {
            await _context.BagTypes.AddAsync(bagType, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task UpdateAsync(BagType.Domain.Entities.BagType bagType, CancellationToken cancellationToken)
        {
            _context.BagTypes.Update(bagType);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(BagType.Domain.Entities.BagType bagType, CancellationToken cancellationToken)
        {
            bagType.IsActive = false; // Soft delete
            _context.BagTypes.Update(bagType);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}