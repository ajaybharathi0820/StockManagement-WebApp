using Common.Middlewares;

namespace StockManagement.API.Extensions
{
    public static class SharedBuilderExtensions
    {
        public static IApplicationBuilder UseShared(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseMiddleware<RequestLoggingMiddleware>();
            return app;
        }
    }
}
