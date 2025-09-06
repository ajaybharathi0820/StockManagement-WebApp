using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Production.API.Controllers;
using Production.Application.Extensions;
using Production.Infrastructure.Extensions;

namespace Production.API.Extensions
{
    public static class ProductionModuleRegistration
    {
        public static IServiceCollection AddProductionModule(this IServiceCollection services, string connectionString)
        {
            services.AddApplicationServices();

            services.AddInfrastructureServices(connectionString);

            services.AddControllers().AddApplicationPart(typeof(PolisherAssignmentsController).Assembly);

            return services;
        }
    }
}