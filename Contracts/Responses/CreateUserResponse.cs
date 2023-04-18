using Common.Constants;
using Data.Models;

namespace Contracts.Responses
{
    public class CreateUserResponse
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Username { get; init; }
        public string Email { get; init; }
        public string Phone { get; init; }
        public string CityName { get; init; }
        public string CountyName { get; init; }
        public string CountryName { get; init; }
        public double? Latitude { get; init; }
        public double? Longitude { get; init; }
    }

    public static partial class ContractMappings
    {
        public static CreateUserResponse ToDto(this User model)
        {
            return new CreateUserResponse
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Username,
                Email = model.Email,
                Phone = model.Phone,
                CityName = model.City?.Name ?? Messages.NoInformationAvailable,
                CountyName = model.City?.County?.Name ?? Messages.NoInformationAvailable,
                CountryName = model.City?.County?.Country?.Name ?? Messages.NoInformationAvailable,
                Latitude = model.PreciseLocation?.Latitude ?? null,
                Longitude = model.PreciseLocation?.Longitude ?? null,
            };
        }
    }
}
