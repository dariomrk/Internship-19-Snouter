namespace Data.Models
{
    public class City : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public County County { get; set; } = null!;
        public int CountyId { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public City() { }
        public City(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
