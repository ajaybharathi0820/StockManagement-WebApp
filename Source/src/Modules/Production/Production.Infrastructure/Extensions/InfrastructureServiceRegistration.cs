using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Production.Domain.Repositories;

namespace Production.Infrastructure.Extensions
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<Persistence.ProductionDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IPolisherAssignmentRepository, Repositories.PolisherAssignmentRepository>();

            return services;
        }
    }
}