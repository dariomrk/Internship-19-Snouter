using Data.Models;

namespace Contracts.Requests
{
    public class CreateCountryRequest
    {
        public class County
        {
            public class City
            {
                public string Name { get; set; }
            }

            public string Name { get; set; }
            public ICollection<City> Cities { get; set; } = new List<City>();
        }
        public string Name { get; set; }
        public ICollection<County> Counties { get; set; } = new List<County>();
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
                Counties = dto.Counties.Select(county => new County
                {
                    Name = county.Name
                    .Trim()
                    .ToLower()
                    .Normalize(),
                    Cities = county.Cities.Select(city => new City
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
