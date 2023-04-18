namespace Data.Models
{
    public class Currency : BaseEntity<int>
    {
        public string Abbreviation { get; set; } = null!;
        public ICollection<Product> Products { get; set; } = null!;
    }
}
