using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BagType.API.Controllers;
using BagType.Infrastructure.Extensions;
using BagType.Application.Extensions;

namespace BagType.API.Extensions
{
    public static class BagTypeModuleRegistration
    {
        public static IServiceCollection AddBagTypeModule(this IServiceCollection services, string connectionString)
        {
            services.AddApplicationServices();

            services.AddInfrastructureServices(connectionString);

            services.AddControllers().AddApplicationPart(typeof(BagTypeController).Assembly);

            return services;
        }
    }
}