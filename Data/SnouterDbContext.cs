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
            #region Ignore BaseEntity<TId>
            modelBuilder.Ignore<BaseEntity<int>>();
            #endregion

            #region User configuration
            var userEntity = modelBuilder.Entity<User>();

            userEntity
                .Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            userEntity
                .Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(50);

            userEntity
                .Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(100);

            userEntity
                .Property(x => x.Username)
                .IsRequired()
                .HasMaxLength(100);

            userEntity
                .Property(x => x.Phone)
                .HasMaxLength(10)
                .IsFixedLength(true);
            #endregion
        }
    }
}
