using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace Api
{
    public static class Logger
    {
        private const string _dateTimeFormat = "dd/MM/yyyy HH:mm:ss";
        private static IConfiguration? _configuration;

        public static void Configure(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }

        public static void StartLogging(Action loggedAction)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(_configuration)
                .WriteTo.Console()
                .WriteTo.File(new JsonFormatter(),
                    $"Logs/log-{DateTime.UtcNow.Ticks}.json",
                    restrictedToMinimumLevel: LogEventLevel.Warning)
                .CreateLogger();

            Log.Information($"Started at: {DateTime.UtcNow.ToString(_dateTimeFormat)}");

            loggedAction();

            Log.Information($"Shut down at: {DateTime.UtcNow.ToString(_dateTimeFormat)}");
            Log.CloseAndFlush();
        }
    }
}
