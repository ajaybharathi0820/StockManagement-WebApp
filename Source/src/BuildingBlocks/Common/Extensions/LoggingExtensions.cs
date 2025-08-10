using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.ApplicationInsights.TelemetryConverters;

namespace Common.Extensions
{
    public static class LoggingExtensions
    {
        const string DefaultLogPath = "C:\\Users\\ajay.kesav\\Documents\\StockManagementLogs\\log-.txt";

        public static void ConfigureSerilog(this IHostBuilder hostBuilder, IConfiguration configuration, string environment)
        {
            var logFilePath = configuration["Logging:FilePath"];

            var loggerConfig = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning);

            if (environment == "Development")
            {
                loggerConfig
                    .WriteTo.Console()
                    .WriteTo.File(
                        logFilePath ?? DefaultLogPath,
                        rollingInterval: RollingInterval.Day
                    );
            }
            else
            {
                loggerConfig
                    .WriteTo.Console()
                    .WriteTo.ApplicationInsights(
                        configuration["ApplicationInsights:ConnectionString"],
                        TelemetryConverter.Traces
                    );
            }

            Log.Logger = loggerConfig.CreateLogger();
            hostBuilder.UseSerilog();
        }
    }
}
