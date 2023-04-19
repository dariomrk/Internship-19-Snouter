using Data.Models;

namespace Contracts.Responses
{
    public class CityResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public static partial class ContractMappings
    {
        public static CityResponse ToDto(this City model)
        {
            return new CityResponse
            {
                Id = model.Id,
                Name = model.Name,
            };
        }
    }
}
