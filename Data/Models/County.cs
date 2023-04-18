namespace Data.Models
{
    public class County : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public Country Country { get; set; } = null!;
    }
}
