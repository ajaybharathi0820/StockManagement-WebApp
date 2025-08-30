using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.API.Controllers;
using Product.Application.Extensions;
using Product.Infrastructure.Extensions;

namespace Product.API.Extensions
{
    public static class ProductModuleRegistration
    {
        public static IServiceCollection AddProductModule(this IServiceCollection services, string connectionString)
        {
            services.AddApplicationServices();

            services.AddInfrastructureServices(connectionString);

            services.AddControllers().AddApplicationPart(typeof(ProductController).Assembly);

            return services;
        }
    }
}