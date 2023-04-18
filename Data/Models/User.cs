namespace Data.Models
{
    public class User : BaseEntity<int>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public City City { get; set; } = null!;
        public PreciseLocation? PreciseLocation { get; set; } = null!;
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
