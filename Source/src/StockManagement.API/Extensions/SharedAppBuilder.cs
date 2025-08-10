using Common.Middlewares;

namespace StockManagement.API.Extensions
{
    public static class SharedAppBuilder
    {
        public static IApplicationBuilder UseShared(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            return app;
        }
    }
}
