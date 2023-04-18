namespace Data.Models
{
    public enum LocationType
    {
        UserLocation,
        ProductLocation,
    }

    public class PreciseLocation : BaseEntity<int>
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public LocationType LocationType { get; set; }
        public ICollection<User> User { get; set; } = new List<User>();
        public ICollection<Product> Product { get; set; } = new List<Product>();
    }
}
