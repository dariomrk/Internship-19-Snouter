using Contracts.Requests;
using Contracts.Responses;
using Data.Models;

namespace Application.Interfaces
{
    public interface ICityService : IService<City, int>
    {
        Task<CityResponse> CreateAsync(int countyId, CreateCityRequest newCityDetails, CancellationToken cancellationToken = default);
        Task UpdateNameAsync(int countyId, int cityId, string newName, CancellationToken cancellationToken = default);
    }
}
