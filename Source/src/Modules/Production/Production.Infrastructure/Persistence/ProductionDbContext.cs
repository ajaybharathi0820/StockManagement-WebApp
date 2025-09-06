using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Production.Domain.Entities;

namespace Production.Infrastructure.Persistence
{
    public class ProductionDbContext : DbContext
    {
        public ProductionDbContext(DbContextOptions<ProductionDbContext> options)
        : base(options)
        {
        }
        public DbSet<PolisherAssignment> PolisherAssignments { get; set; }
        public DbSet<PolisherAssignmentItem> PolisherAssignmentItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PolisherAssignment>(entity =>
            {
                entity.HasKey(pa => pa.Id);

                entity.HasMany(pa => pa.Items)
                    .WithOne(i => i.Assignment)
                    .HasForeignKey(i => i.AssignmentId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Metadata
                    .FindNavigation(nameof(PolisherAssignment.Items))!
                    .SetPropertyAccessMode(PropertyAccessMode.Field);
            });

            modelBuilder.Entity<PolisherAssignmentItem>(entity =>
            {
                entity.HasKey(pai => pai.Id);
            });

        }
    }
}