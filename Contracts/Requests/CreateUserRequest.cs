using Data.Models;

namespace Contracts.Requests
{
    public class CreateUserRequest
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
        public static User ToModel(this CreateUserRequest dto)
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
                    .Trim()
                    .ToLower()
                    .Normalize(),
                Email = dto.Email
                    .Trim()
                    .ToLower()
                    .Normalize(),
                Phone = dto.Phone
                    .Trim()
                    .Normalize(),
                City = new City
                {
                    Name = dto.CityName
                        .Trim()
                        .ToLower()
                        .Normalize(),
                    County = new County
                    {
                        Name = dto.CountyName
                            .Trim()
                            .ToLower()
                            .Normalize(),
                        Country = new Country
                        {
                            Name = dto.CountryName
                            .Trim()
                            .ToLower()
                            .Normalize(),
                        }
                    }
                },
                PreciseLocation = dto.Latitude.HasValue && dto.Longitude.HasValue
                ? new PreciseLocation
                {
                    Latitude = dto.Latitude.Value,
                    Longitude = dto.Longitude.Value,
                }
                : null,
            };
        }
    }
}
