namespace Data.Models
{
    public class Category : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
    }
}
