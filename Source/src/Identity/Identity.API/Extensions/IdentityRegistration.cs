using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Infrastructure.Extensions;
using Identity.API.Controllers;

namespace Identity.API.Extensions
{
    public static class IdentityRegistration
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration,string connectionString)
        {
            services.AddIdentityAuthentication(configuration);
            services.AddInfrastructureServices(connectionString);
            services.AddControllers()
            .AddApplicationPart(typeof(UsersController).Assembly)
            .AddApplicationPart(typeof(RolesController).Assembly);

            return services;
        }
    }
}