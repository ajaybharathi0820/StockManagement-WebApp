using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Identity.Domain.Repositories;
using Identity.Domain.Auth;

namespace Identity.Infrastructure.Extensions
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<Persistence.IdentityDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IUserRepository, Repositories.UserRepository>();
            services.AddScoped<IRoleRepository, Repositories.RoleRepository>();
            services.AddScoped<IJwtTokenGenerator, Auth.JwtTokenGenerator>();

            return services;
        }
    }
}