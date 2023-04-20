using Application.Interfaces;
using Common.Constants;
using Contracts.Requests;
using Contracts.Responses;
using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class CityService : BaseService<City, int>, ICityService
    {
        private readonly IRepository<County, int> _countyRepository;
        public CityService(
            IRepository<City, int> repository,
            IRepository<County, int> countyRepository) : base(repository)
        {
            _countyRepository = countyRepository;
        }

        public async Task<CityResponse> CreateAsync(
            int countyId,
            CreateCityRequest newCityRequest,
            CancellationToken cancellationToken = default)
        {
            var county = await _countyRepository.FindAsync(countyId, cancellationToken);

            if (county is null)
                throw new ArgumentException(Messages.CountyInvalid);

            var mapped = newCityRequest.ToModel(county.Id);

            var creationResult = await _repository.CreateAsync(mapped, cancellationToken);

            if (creationResult.RepositoryActionResult is not Data.Enums.RepositoryAction.Success)
                throw new Exception(Messages.RepositoryActionFailed);

            return creationResult.CreatedEntity.ToDto();
        }

        public async Task UpdateNameAsync(
            int countyId,
            int cityId,
            string newName,
            CancellationToken cancellationToken = default)
        {
            var county = await _countyRepository.FindAsync(countyId, cancellationToken);

            if (county is null)
                throw new ArgumentException(Messages.CountyInvalid);

            var city = await _repository
                .Query()
                .FirstOrDefaultAsync(c => c.Id == cityId && c.CountyId == countyId, cancellationToken);

            city!.Name = newName;

            await _repository.UpdateAsync(city, cancellationToken);
        }
    }
}
