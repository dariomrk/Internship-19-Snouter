using Data.Models;

namespace Contracts.Responses
{
    public class CreateCountryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public static partial class ContractMappings
    {
        public static CreateCountryResponse ToCreateCountryResponse(this Country model)
        {
            return new CreateCountryResponse
            {
                Id = model.Id,
                Name = model.Name,
            };
        }
    }
}
