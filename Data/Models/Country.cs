namespace Data.Models
{
    public class Country : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public ICollection<County> Counties { get; set; } = null!;
    }
}
