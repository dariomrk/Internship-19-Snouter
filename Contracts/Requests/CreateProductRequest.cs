using Data.Models;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Contracts.Requests
{
    public class CreateProductRequest
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CurrencyId { get; set; }
        public int CityId { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public ProductState State { get; set; }
        public int SubCategoryId { get; set; }
        public JsonObject Properties { get; set; }
        public int CreatorId { get; set; }
        public IEnumerable<string> ImagesBase64 { get; set; } = new List<string>();
    }

    public static partial class ContractMappings
    {
        public static Product ToModel(this CreateProductRequest dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Slug = dto.Slug,
                Description = dto.Description,
                Price = dto.Price,
                CurrencyId = dto.CurrencyId,
                CityId = dto.CityId,
                PreciseLocation = dto.Latitude.HasValue && dto.Longitude.HasValue
                ? new PreciseLocation
                {
                    Latitude = dto.Latitude.Value,
                    Longitude = dto.Longitude.Value,
                }
                : null,
                State = dto.State,
                Availability = ProductAvailability.Available,
                SubCategoryId = dto.SubCategoryId,
                Properties = JsonDocument.Parse(dto.Properties.ToString()),
                CreatorId = dto.CreatorId,
                PublishedAt = DateTime.UtcNow,
                RenewedAt = DateTime.UtcNow,
                Images = dto.ImagesBase64.Select(base64 => new Image
                {
                    ImageBase64 = base64,
                })
                .ToList(),
            };

            return product;
        }
    }
}
