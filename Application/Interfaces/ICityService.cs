using Data.Models;

namespace Application.Interfaces
{
    public interface ICityService : IService<City, int>
    {
        Task<City?> FindByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<City> FindOrCreateAsync(string cityName, string countyName, string countryName, CancellationToken cancellationToken = default);
    }
}
