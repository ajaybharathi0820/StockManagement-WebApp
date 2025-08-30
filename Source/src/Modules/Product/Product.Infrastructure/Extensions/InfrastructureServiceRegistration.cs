using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Product.Domain.Repositories;

namespace Product.Infrastructure.Extensions
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<Persistence.ProductDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IProductRepository, Repositories.ProductRepository>();

            return services;
        }
    }
}