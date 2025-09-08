using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace Identity.Infrastructure.Persistence
{
    public static class IdentityDataSeeder
    {
        public static async Task SeedAsync(IdentityDbContext context)
        {
            Console.WriteLine("🌱 Starting Identity data seeding...");
            
            // Ensure database is created and migrations are applied
            Console.WriteLine("📊 Applying migrations...");
            await context.Database.MigrateAsync();
            Console.WriteLine("✅ Migrations applied successfully");

            // Seed Roles if they don't exist
            var existingRolesCount = await context.Roles.CountAsync();
            Console.WriteLine($"📋 Found {existingRolesCount} existing roles");
            
            if (existingRolesCount == 0)
            {
                Console.WriteLine("🔧 Seeding roles...");
                var adminRole = new Role("Admin");
                var associateRole = new Role("Associate");

                await context.Roles.AddRangeAsync(adminRole, associateRole);
                await context.SaveChangesAsync();
                Console.WriteLine("✅ Roles seeded successfully");
            }
            else
            {
                Console.WriteLine("ℹ️ Roles already exist, skipping role seeding");
            }

            // Check and seed users separately (regardless of roles)
            var existingUsersCount = await context.Users.CountAsync();
            Console.WriteLine($"👥 Found {existingUsersCount} existing users");
            
            if (existingUsersCount == 0)
            {
                // Seed default admin user after roles are saved
                Console.WriteLine("👤 Checking if admin user already exists...");
                var existingAdmin = await context.Users.FirstOrDefaultAsync(u => u.UserName == "admin");
                
                if (existingAdmin == null)
                {
                    Console.WriteLine("👤 Seeding admin user...");
                    var savedAdminRole = await context.Roles.FirstAsync(r => r.Name == "Admin");
                    Console.WriteLine($"🔍 Found admin role with ID: {savedAdminRole.Id}");
                    
                    var adminUser = new User(
                        "System",
                        "Admin", 
                        "admin",
                        new DateTime(1990, 1, 1),
                        "admin@stockmanagement.com",
                        BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                        savedAdminRole.Id
                    );

                    Console.WriteLine("🔧 Adding admin user to context...");
                    
                    try
                    {
                        await context.Users.AddAsync(adminUser);
                        var result = await context.SaveChangesAsync();
                        Console.WriteLine($"✅ Admin user seeded successfully. Records affected: {result}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ Error saving admin user: {ex.Message}");
                        Console.WriteLine($"Inner exception: {ex.InnerException?.Message}");
                        throw;
                    }
                }
                else
                {
                    Console.WriteLine("ℹ️ Admin user already exists, skipping...");
                }
            }
            else
            {
                Console.WriteLine("ℹ️ Users already exist, skipping user seeding");
            }
        }
    }
}
