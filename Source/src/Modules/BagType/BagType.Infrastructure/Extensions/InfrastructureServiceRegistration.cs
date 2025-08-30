using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BagType.Domain.Repositories;

namespace BagType.Infrastructure.Extensions
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<Persistence.BagTypeDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IBagTypeRepository, Repositories.BagTypeRepository>();

            return services;
        }
    }
}