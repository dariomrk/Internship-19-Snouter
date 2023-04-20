using Data.Models;
using System.Text.Json;

namespace Contracts.Requests
{
    public class CreateSubCategoryRequest
    {
        public string Name { get; set; }
        public JsonDocument ValidationSchema { get; set; }
    }

    public static partial class ContractMappings
    {
        public static SubCategory ToModel(this CreateSubCategoryRequest request, int categoryId)
        {
            return new SubCategory
            {
                Name = request.Name,
                ValidationSchema = request.ValidationSchema,
                CategoryId = categoryId,
            };
        }
    }
}
