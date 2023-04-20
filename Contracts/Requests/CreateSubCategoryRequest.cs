using Data.Models;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Contracts.Requests
{
    public class CreateSubCategoryRequest
    {
        public string Name { get; set; }
        public JsonObject ValidationSchema { get; set; }
    }

    public static partial class ContractMappings
    {
        public static SubCategory ToModel(this CreateSubCategoryRequest model, int categoryId)
        {
            return new SubCategory
            {
                Name = model.Name,
                ValidationSchema = JsonDocument.Parse(model.ValidationSchema.ToString()),
                CategoryId = categoryId,
            };
        }
    }
}
