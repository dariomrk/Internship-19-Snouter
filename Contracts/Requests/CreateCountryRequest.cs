using Data.Models;

namespace Contracts.Requests
{
    public class CreateCountryRequest
    {
        public string Name { get; set; }
        public ICollection<CreateCountyRequest> Counties { get; set; } = new List<CreateCountyRequest>();
    }

    public static partial class ContractMappings
    {
        public static Country ToModel(this CreateCountryRequest dto)
        {
            return new Country
            {
                Name = dto.Name
                    .Trim()
                    .ToLower()
                    .Normalize(),
                Counties = dto.Counties
                    .Select(county => new County
                    {
                        Name = county.Name
                    .Trim()
                    .ToLower()
                    .Normalize(),
                        Cities = county.Cities
                        .Select(city => new City
                        {
                            Name = city.Name
                        .Trim()
                        .ToLower()
                        .Normalize(),
                        })
                    .ToList()
                    })
                .ToList()
            };
        }
    }
}
