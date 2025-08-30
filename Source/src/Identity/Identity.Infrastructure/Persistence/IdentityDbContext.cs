using Microsoft.EntityFrameworkCore;
using Identity.Domain.Entities;
using BCrypt.Net;

namespace Identity.Infrastructure.Persistence
{
    public class IdentityDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}