using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using System.Reflection;
using Common.Behaviors;
using Common.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Extensions
{
    public static class SharedServiceExtensions
    {
        public static IServiceCollection UseSharedServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            services.AddMediatR(cfg =>
            {
                foreach (var assembly in assemblies)
                {
                    cfg.RegisterServicesFromAssembly(assembly);
                }
            });

            foreach (var assembly in assemblies)
            {
                services.AddValidatorsFromAssembly(assembly);
            }

            services.AddControllers(options=>{
                options.Filters.Add<ApiResponseWrapperFilter>();
            });

            return services;
        }
    }
}