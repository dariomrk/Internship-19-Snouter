using Data.Constants;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    internal static class ModelConfiguration
    {
        internal static ModelBuilder ConfigureCategory(this ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<Category>();

            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(64);

            return modelBuilder;
        }
        internal static ModelBuilder ConfigureCity(this ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<City>();

            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(64);

            return modelBuilder;
        }
        internal static ModelBuilder ConfigureCountry(this ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<Country>();

            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(64);

            return modelBuilder;
        }
        internal static ModelBuilder ConfigureCounty(this ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<County>();

            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(64);

            return modelBuilder;
        }
        internal static ModelBuilder ConfigureCurrency(this ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<Currency>();

            builder
                .Property(x => x.Abbreviation)
                .IsRequired()
                .HasMaxLength(3);

            return modelBuilder;
        }
        internal static ModelBuilder ConfigureImage(this ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<Image>();

            builder
                .Property(x => x.ImageBase64)
                .IsRequired()
                .HasColumnType("text");

            return modelBuilder;
        }
        internal static ModelBuilder ConfigurePreciseLocation(this ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<PreciseLocation>();

            builder
                .Property(x => x.Latitude)
                .IsRequired();

            builder
                .Property(x => x.Longitude)
                .IsRequired();

            builder
                .Property(x => x.LocationType)
                .IsRequired();

            return modelBuilder;
        }
        internal static ModelBuilder ConfigureProduct(this ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<Product>();

            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(48);

            builder
                .Property(x => x.Slug)
                .IsRequired()
                .HasMaxLength(64);

            builder
                .Property(x => x.Description)
                .IsRequired()
                .HasColumnType("text");

            builder
                .Property(x => x.Price)
                .IsRequired()
                .HasColumnType("money");

            builder
                .Property(x => x.State)
                .IsRequired()
                .HasDefaultValue(ProductState.Unknown);

            builder
                .Property(x => x.Availability)
                .IsRequired()
                .HasDefaultValue(ProductAvailability.Available);

            builder
                .Property(x => x.Properties)
                .IsRequired();

            builder
                .Property(x => x.PublishedAt)
                .IsRequired()
                .HasDefaultValueSql(RawSql.TimestampUtc);

            builder
                .Property(x => x.RenewedAt)
                .IsRequired()
                .HasDefaultValueSql(RawSql.TimestampUtc);

            builder
                .Ignore(x => x.HasExpired);

            return modelBuilder;
        }
        internal static ModelBuilder ConfigureSubCategory(this ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<SubCategory>();

            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(32);

            return modelBuilder;
        }
        internal static ModelBuilder ConfigureUser(this ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<User>();

            builder
                .Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(x => x.Username)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(x => x.Phone)
                .IsRequired()
                .HasMaxLength(10)
                .IsFixedLength(true);

            return modelBuilder;
        }
    }
}
