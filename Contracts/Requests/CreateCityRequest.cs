using Data.Models;

namespace Contracts.Requests
{
    public class CreateCityRequest
    {
        public string Name { get; set; }
    }

    public static partial class ContractMappings
    {
        public static City ToModel(this CreateCityRequest dto, int countyId)
        {
            return new City
            {
                Name = dto.Name,
                CountyId = countyId,
            };
        }

    }
}
