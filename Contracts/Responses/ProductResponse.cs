using Common.Constants;
using Data.Models;
using System.Text.Json;

namespace Contracts.Responses
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public ProductState ProductState { get; set; }
        public ProductAvailability ProductAvailability { get; set; }
        public string SubCategoryName { get; set; }
        public string CategoryName { get; set; }
        public JsonElement Properties { get; set; }
        public int CreatorId { get; set; }
        public DateTime PublishedAt { get; set; }
        public DateTime RenewedAt { get; set; }
    }

    public static partial class ContractMappings
    {
        public static ProductResponse ToDto(this Product model)
        {
            return new ProductResponse
            {
                Id = model.Id,
                Name = model.Name,
                Slug = model.Slug,
                Description = model.Description,
                Price = model.Price,
                Currency = model.Currency?.Abbreviation ?? Messages.NoInformationAvailable,
                CategoryName = model.SubCategory?.Category?.Name ?? Messages.NoInformationAvailable,
                SubCategoryName = model.SubCategory?.Name ?? Messages.NoInformationAvailable,
                City = model.City?.Name ?? Messages.NoInformationAvailable,
                County = model.City?.County?.Name ?? Messages.NoInformationAvailable,
                Country = model.City?.County?.Country?.Name ?? Messages.NoInformationAvailable,
                CreatorId = model.CreatorId,
                PublishedAt = model.PublishedAt,
                Latitude = model.PreciseLocation?.Latitude ?? null,
                Longitude = model.PreciseLocation?.Longitude ?? null,
                ProductAvailability = model.Availability,
                ProductState = model.State,
                RenewedAt = model.RenewedAt,
                Properties = model.Properties.RootElement,
            };
        }
    }
}
