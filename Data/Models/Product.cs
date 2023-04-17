using System.Text.Json;

namespace Data.Models
{
    public enum ProductState
    {
        New,
        Used,
        Damaged,
        NonFunctional,
    }

    public enum ProductAvailability
    {
        Available,
        Unavailable,
        Sold,
    }

    public class Product : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public Currency Currency { get; set; } = null!;
        public Location Location { get; set; } = null!;
        public ProductState State { get; set; }
        public ProductAvailability Availability { get; set; }
        public SubCategory SubCategory { get; set; } = null!;
        public JsonDocument Properties { get; set; } = null!;
        public User Creator { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime RenewedAt { get; set; }
        public bool HasExpired { get; set; }
    }
}
