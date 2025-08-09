using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Polisher.Domain.Repositories;

namespace Polisher.Infrastructure.Extensions
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<Persistence.PolisherDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IPolisherRepository, Repositories.PolisherRepository>();

            return services;
        }
    }
}