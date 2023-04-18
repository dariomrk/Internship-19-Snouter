using Common.Constants;
using Data.Models;

namespace Contracts.Responses
{
    public class UserResponse
    {
        public class UserResponseProduct
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public ProductAvailability Availability { get; set; }
            public bool HasExpired { get; set; }
        }
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
        public ICollection<UserResponseProduct> Products { get; set; } = new List<UserResponseProduct>();
    }

    public static partial class ContractMappings
    {
        public static UserResponse ToUserResponse(this User model)
        {
            return new UserResponse
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
                Products = model.Products
                    .Select(p => new UserResponse.UserResponseProduct
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Availability = p.Availability,
                        HasExpired = p.HasExpired,
                    })
                    .ToList(),
            };
        }
    }
}
