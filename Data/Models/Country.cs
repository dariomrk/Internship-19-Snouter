namespace Data.Models
{
    public class Country : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public ICollection<County> Counties { get; set; } = null!;
        public Country() { }
        public Country(int id, string name, ICollection<County> counties)
        {
            Id = id;
            Name = name;
            Counties = counties;
        }
    }
}
