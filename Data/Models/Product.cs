using System.Text.Json;

namespace Data.Models
{
    public enum ProductState
    {
        New,
        Used,
        Damaged,
        NonFunctional,
        Unknown,
    }

    public enum ProductAvailability
    {
        Available,
        Unavailable,
        Sold,
    }

    public class Product : BaseEntity<int>, IDisposable
    {
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public Currency Currency { get; set; } = null!;
        public int CurrencyId { get; set; }
        public City City { get; set; } = null!;
        public int CityId { get; set; }
        public PreciseLocation? PreciseLocation { get; set; }
        public ProductState State { get; set; }
        public ProductAvailability Availability { get; set; }
        public SubCategory SubCategory { get; set; } = null!;
        public int SubCategoryId { get; set; }
        public JsonDocument Properties { get; set; } = null!;
        public User Creator { get; set; } = null!;
        public int CreatorId { get; set; }
        public DateTime PublishedAt { get; set; }
        public DateTime RenewedAt { get; set; }
        public bool HasExpired => RenewedAt.AddDays(30) < DateTime.UtcNow;
        public ICollection<Image> Images { get; set; } = new List<Image>();

        public void Dispose() => Properties?.Dispose();
    }
}
