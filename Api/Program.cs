using Serilog;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.ConfigureHost();
            builder.Services.RegisterApplicationServices();

            var app = builder.Build();

            app.ConfigureMiddleware();

            Logger.Configure(builder.Configuration);
            Logger.StartLogging(()=> { app.Run(); });
        }
    }

    public static class HostInitializer
    {
        public static IHostBuilder ConfigureHost(this IHostBuilder host)
        {
            host.UseSerilog();
            return host;
        }
    }

    public static class ServiceInitializer
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }

    public static class MiddlewareInitializer
    {
        public static WebApplication ConfigureMiddleware(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            return app;
        }
    }
}