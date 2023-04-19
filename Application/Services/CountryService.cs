using Application.Interfaces;
using Common.Constants;
using Contracts.Requests;
using Contracts.Responses;
using Data.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class CountryService : BaseService<Country, int>, ICountryService
    {
        private readonly IRepository<Country, int> _countryRepository;
        private readonly IRepository<County, int> _countyRepository;
        private readonly IRepository<City, int> _cityRepository;

        public CountryService(
            IRepository<Country, int> countryRepository,
            IRepository<County, int> countyRepository,
            IRepository<City, int> cityRepository) : base(countryRepository)
        {
            _countryRepository = countryRepository;
            _countyRepository = countyRepository;
            _cityRepository = cityRepository;
        }

        public async Task<CountryResponse> CreateAsync([FromBody] CreateCountryRequest request, CancellationToken cancellationToken = default)
        {
            var mapped = request.ToModel();

            var country = await _countryRepository
                .Query()
                .FirstOrDefaultAsync(c => c.Name.Contains(mapped.Name), cancellationToken);

            if (country is not null)
                throw new InvalidOperationException(Messages.IdentityInformationInUse);

            var creationResult = await _countryRepository.CreateAsync(mapped, cancellationToken);

            if (creationResult.RepositoryActionResult is not Data.Enums.RepositoryAction.Success)
                throw new Exception(Messages.RepositoryActionFailed);

            return creationResult.CreatedEntity.ToDto();

        }
    }
}
