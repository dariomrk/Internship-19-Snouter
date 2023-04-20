using Data.Models;

namespace Contracts.Responses
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> SubCategories = new List<string>();
    }

    public static partial class ContractMappings
    {
        public static CategoryResponse ToDto(this Category model)
        {
            return new CategoryResponse
            {
                Id = model.Id,
                Name = model.Name,
                SubCategories = model.SubCategories.Select(x => x.Name)
            };
        }
    }
}
