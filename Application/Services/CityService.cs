using Application.Interfaces;
using Common.Constants;
using Contracts.Requests;
using Contracts.Responses;
using Data.Interfaces;
using Data.Models;

namespace Application.Services
{
    public class CityService : BaseService<City, int>, ICityService
    {
        private readonly ICountyService _countyService;
        public CityService(
            IRepository<City, int> repository,
            ICountyService countyService) : base(repository)
        {
            _countyService = countyService;
        }

        public async Task<CityResponse> CreateAsync(
            int countyId,
            CreateCityRequest newCityRequest,
            CancellationToken cancellationToken = default)
        {
            var county = await _countyService.FindAsync(countyId, cancellationToken);

            if (county is null)
                throw new ArgumentException(Messages.CountyInvalid);

            var mapped = newCityRequest.ToModel(county.Id);

            var creationResult = await _repository.CreateAsync(mapped, cancellationToken);

            if (creationResult.RepositoryActionResult is not Data.Enums.RepositoryAction.Success)
                throw new Exception(Messages.RepositoryActionFailed);

            return creationResult.CreatedEntity.ToDto();
        }
    }
}
