using Common.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class SnouterDbContextFactory : IDesignTimeDbContextFactory<SnouterDbContext>
    {
        public SnouterDbContext CreateDbContext(string[] args)
        {
            var connectionString = ConfigurationHelper
                .GetConfiguration()
                .GetConnectionString("Database")
                ?? throw new ArgumentNullException("Database connection string is not specified.");

            var options = new DbContextOptionsBuilder<SnouterDbContext>()
                .UseNpgsql(connectionString)
                .Options;

            return new SnouterDbContext(options);
        }
    }
}
