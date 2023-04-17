namespace Data.Models
{
    public enum LocationType
    {
        UserLocation,
        ProductLocation,
        Settlement,
    }

    public class Location : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public LocationType LocationType { get; set; }
        public County County { get; set; } = null!;
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
