using Data.Models;

namespace Contracts.Responses
{
    public class CountyResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<CityResponse> Cities { get; set; } = new List<CityResponse>();
    }
    public static partial class ContractMappings
    {
        public static CountyResponse ToDto(this County model)
        {
            return new CountyResponse
            {
                Id = model.Id,
                Name = model.Name,
                Cities = model.Cities.Select(c => c.ToDto()),
            };
        }
    }
}
