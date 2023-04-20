using Application.Interfaces;
using Json.Schema;
using System.Text.Json;

public class JsonSchemaValidationService : IJsonSchemaValidationService
{
    public bool ValidateSchema(JsonDocument toValidate, JsonDocument validationSchema)
    {
        var schema = JsonSchema.FromText(validationSchema.RootElement.GetString());
        var validationResult = schema.Evaluate(toValidate.RootElement.GetString());

        return validationResult.IsValid;
    }
}
