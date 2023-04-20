using Api.Middleware;
using Application.Interfaces;
using Application.Services;
using Common.Helpers;
using Data;
using Data.Interfaces;
using Data.Models;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text.Json;
using System.Text.Json.Serialization;

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
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                });
            #endregion

            #region Service registration
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICountyService, CountyService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IPreciseLocationService, PreciseLocationService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISubCategoryservice, SubCategoryService>();
            services.AddScoped<IUserService, UserService>();
            #endregion

            #region Repository registration
            services.AddScoped<IRepository<Category, int>, BaseRepository<Category, int>>();
            services.AddScoped<IRepository<City, int>, BaseRepository<City, int>>();
            services.AddScoped<IRepository<Country, int>, BaseRepository<Country, int>>();
            services.AddScoped<IRepository<County, int>, BaseRepository<County, int>>();
            services.AddScoped<IRepository<Currency, int>, BaseRepository<Currency, int>>();
            services.AddScoped<IRepository<PreciseLocation, int>, BaseRepository<PreciseLocation, int>>();
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
            services.AddSwaggerGen(config =>
            {
                config.MapType<JsonDocument>(() => new OpenApiSchema
                {
                    Type = "object",
                    Format = "json",
                });
            });

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

            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            return app;
        }
    }
}
