using Common.Extensions;
using Data.Models;

namespace Contracts.Requests
{
    public class UserRequest
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
        public static User ToModel(this UserRequest dto)
        {
            return new User
            {
                FirstName = dto.FirstName
                     .Trim()
                     .Normalize(),
                LastName = dto.LastName
                    .Trim()
                    .Normalize(),
                Username = dto.Username
                    .Sanitize(),
                Email = dto.Email
                    .Sanitize(),
                Phone = dto.Phone
                    .Sanitize(),
                PreciseLocation = dto.Latitude.HasValue && dto.Longitude.HasValue
                ? new PreciseLocation
                {
                    Latitude = dto.Latitude.Value,
                    Longitude = dto.Longitude.Value,
                    LocationType = LocationType.UserLocation,
                }
                : null,
            };
        }
    }
}
