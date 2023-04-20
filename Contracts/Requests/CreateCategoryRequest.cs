using Data.Models;

namespace Contracts.Requests
{
    public class CreateCategoryRequest
    {
        public string Name { get; set; }
    }

    public static partial class ContractMappings
    {
        public static Category ToModel(this CreateCategoryRequest dto)
        {
            return new Category
            {
                Name = dto.Name,
            };
        }
    }
}
