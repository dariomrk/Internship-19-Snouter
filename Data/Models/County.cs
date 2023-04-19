namespace Data.Models
{
    public class County : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public Country Country { get; set; } = null!;
        public int CountryId { get; set; }
        public ICollection<City> Cities { get; set; } = new List<City>();
        public County() { }
        public County(int id, string name, ICollection<City> cities)
        {
            Id = id;
            Name = name;
            Cities = cities;
        }
    }
}
