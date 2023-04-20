using System.Text.Json;

namespace Application.Interfaces
{
    public interface IJsonSchemaValidationService
    {
        bool ValidateSchema(JsonDocument toValidate, JsonDocument validationSchema);
    }
}
