using Microsoft.Extensions.Configuration;

namespace Common.Helpers
{
    public class ConfigurationHelper
    {
        /// <summary>
        /// Gets the configuration from the application.<i>&lt;ASPNETCORE_ENVIRONMENT&gt;</i>.json file.<br/>
        /// To set the desired environment run the following command in the Package Manager Console:
        /// <code>
        /// $env:ASPNETCORE_ENVIRONMENT = "<i>&lt;ASPNETCORE_ENVIRONMENT&gt;</i>"
        /// </code>
        /// The available environments are located in the <i>appsettings.json</i> configuration file under the "Environments" property.
        /// </summary>
        public static IConfigurationRoot GetConfiguration()
        {
            var environments = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build()
                .GetValue<string>("Environments")
                ?.Split(";")
                ?? throw new InvalidOperationException("Configuration value \"Environments\" is missing in appsettings.json.");
            
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                ?? throw new InvalidOperationException($"Environment variable ASPNETCORE_ENVIRONMENT is not set.");

            if (!environments.Contains(environment))
                throw new ArgumentException("ASPNETCORE_ENVIROMENT is not set to a valid value.");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{environment}.json")
                .Build();

            return configuration;
        }
    }
}
