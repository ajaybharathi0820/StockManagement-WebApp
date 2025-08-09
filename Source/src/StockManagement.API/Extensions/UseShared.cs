using StockManagement.API.Middlewares;

namespace StockManagement.API.Extensions
{
    public static class ExceptionHandlingExtensions
    {
        public static IApplicationBuilder UseShared(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            return app;
        }
    }
}
