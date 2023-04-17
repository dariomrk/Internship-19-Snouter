using System.Text.Json;

namespace Data.Models
{
    public class SubCategory : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public JsonDocument ValidationSchema { get; set; } = null!;
        public Category Category { get; set; } = null!;
    }
}
