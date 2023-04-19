using Data.Models;
using Data.Seeds;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class SnouterDbContext : DbContext
    {
        public SnouterDbContext(DbContextOptions options) : base(options) { }

        #region DbSets
        public DbSet<User> Users => Set<User>();
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<BaseEntity<int>>();

            modelBuilder
                .ConfigureCategory()
                .ConfigureCity()
                .ConfigureCountry()
                .ConfigureCounty()
                .ConfigureCurrency()
                .ConfigurePreciseLocation()
                .ConfigureProduct()
                .ConfigureSubCategory()
                .ConfigureUser()
                .AddCroatia();
        }
    }
}
