using Data.Models;
using System.Text.Json;

namespace Contracts.Responses
{
    public class SubCategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public JsonDocument ValidationSchema { get; set; }
    }

    public static partial class ContractMappings
    {
        public static SubCategoryResponse ToDto(this SubCategory model)
        {
            return new SubCategoryResponse
            {
                Id = model.Id,
                Name = model.Name,
                ValidationSchema = model.ValidationSchema,
            };
        }
    }
}
