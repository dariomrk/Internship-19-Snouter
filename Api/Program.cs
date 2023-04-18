using Application.Interfaces;
using Application.Services;
using Common.Helpers;
using Data;
using Data.Interfaces;
using Data.Models;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
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
            Logger.StartLogging(() => { app.Run(); });
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
            #region Controller registration
            services.AddControllers();
            #endregion

            #region Service registration
            services.AddScoped<ICrudService<Category, int>, CategoryService>();
            services.AddScoped<ICrudService<Country, int>, CountryService>();
            services.AddScoped<ICrudService<County, int>, CountyService>();
            services.AddScoped<ICrudService<Currency, int>, CurrencyService>();
            services.AddScoped<ICrudService<Location, int>, LocationService>();
            services.AddScoped<ICrudService<Product, int>, ProductService>();
            services.AddScoped<ICrudService<SubCategory, int>, SubCategoryService>();
            services.AddScoped<ICrudService<User, int>, UserService>();
            #endregion

            #region Repository registration
            services.AddScoped<IRepository<Category, int>, BaseRepository<Category, int>>();
            services.AddScoped<IRepository<Country, int>, BaseRepository<Country, int>>();
            services.AddScoped<IRepository<County, int>, BaseRepository<County, int>>();
            services.AddScoped<IRepository<Currency, int>, BaseRepository<Currency, int>>();
            services.AddScoped<IRepository<Location, int>, BaseRepository<Location, int>>();
            services.AddScoped<IRepository<Product, int>, BaseRepository<Product, int>>();
            services.AddScoped<IRepository<SubCategory, int>, BaseRepository<SubCategory, int>>();
            services.AddScoped<IRepository<User, int>, BaseRepository<User, int>>();
            #endregion

            var connectionString = ConfigurationHelper
                .GetConfiguration()
                .GetConnectionString("Database");

            services.AddDbContext<SnouterDbContext>(options =>
                options.UseNpgsql(connectionString));

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
