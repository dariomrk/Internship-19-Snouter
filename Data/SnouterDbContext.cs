using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class SnouterDbContext : DbContext
    {
        public SnouterDbContext(DbContextOptions options) : base(options)
        {

        }

        #region DbSets
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
