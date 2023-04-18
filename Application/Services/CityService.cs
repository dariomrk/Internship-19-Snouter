using Application.Interfaces;
using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class CityService : BaseService<City, int>, ICityService
    {
        public CityService(IRepository<City, int> repository) : base(repository) { }

        public async Task<City?> FindByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            var searchedCity = await _repository
                .Query()
                .Where(x => x.Name.Trim().ToLower() == name)
                .FirstOrDefaultAsync(cancellationToken);

            return searchedCity;
        }

        public async Task<City> FindOrCreateAsync(string cityName, string countyName, string countryName, CancellationToken cancellationToken = default)
        {
            var searchedCity = await _repository
                .Query()
                .Where(x => x.Name.Trim().ToLower() == cityName)
                .FirstOrDefaultAsync(cancellationToken);

            if (searchedCity is not null)
            {
                return searchedCity;
            }

            var newCity = new City
            {
                Name = cityName
                    .Trim()
                    .ToLower(),
                County = new County
                {
                    Name = countyName
                        .Trim()
                        .ToLower(),
                    Country = new Country
                    {
                        Name = countryName
                            .Trim()
                            .ToLower(),
                    }
                }
            };

            var repositoryResult = await _repository
                .CreateAsync(newCity, cancellationToken);

            return repositoryResult.CreatedEntity;
        }
    }
}
