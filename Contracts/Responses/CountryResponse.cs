using Data.Models;

namespace Contracts.Responses
{
    public class CountryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Counties { get; set; } = new List<string>();
    }

    public static partial class ContractMappings
    {
        public static CountryResponse ToDto(this Country model)
        {
            return new CountryResponse
            {
                Id = model.Id,
                Name = model.Name,
                Counties = model.Counties.Select(c => c.Name),
            };
        }
    }
}

