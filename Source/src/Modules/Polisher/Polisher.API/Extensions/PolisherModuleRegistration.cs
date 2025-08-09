using Polisher.API.Controllers;
using Polisher.Application.Extensions;
using Polisher.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Polisher.API.Extensions
{
    public static class PolisherModuleRegistration
    {
        public static IServiceCollection AddPolisherModule(this IServiceCollection services, string connectionString)
        {
            services.AddApplicationServices();

            services.AddInfrastructureServices(connectionString);

            services.AddControllers().AddApplicationPart(typeof(PolisherController).Assembly);

            return services;
        }
    }
}