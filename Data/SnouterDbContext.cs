using Data.Models;
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
                .ConfigureCountry()
                .ConfigureCounty()
                .ConfigureCurrency()
                .ConfigureLocation()
                .ConfigureProduct()
                .ConfigureSubCategory()
                .ConfigureUser();
        }
    }
}
