using Application.Interfaces;
using Common.Constants;
using Common.Exceptions;
using Contracts.Requests;
using Contracts.Responses;
using Data.Interfaces;
using Data.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class CityService : BaseService<City, int>, ICityService
    {
        private readonly IRepository<County, int> _countyRepository;
        private readonly IValidator<CreateCityRequest> _createCityRequestValidator;
        public CityService(
            IRepository<City, int> repository,
            IRepository<County, int> countyRepository,
            IValidator<CreateCityRequest> createCityRequestValidator) : base(repository)
        {
            _countyRepository = countyRepository;
            _createCityRequestValidator = createCityRequestValidator;
        }

        public async Task<CityResponse> CreateAsync(
            int countyId,
            CreateCityRequest newCityRequest,
            CancellationToken cancellationToken = default)
        {
            await _createCityRequestValidator.ValidateAndThrowAsync(newCityRequest, cancellationToken);

            var county = await _countyRepository.FindAsync(countyId, cancellationToken);

            if (county is null)
                throw new BadRequestException(Messages.CountyInvalid);

            var mapped = newCityRequest.ToModel(county.Id);

            var creationResult = await _repository.CreateAsync(mapped, cancellationToken);

            if (creationResult.RepositoryActionResult is not Data.Enums.RepositoryAction.Success)
                throw new InvalidOperationException(Messages.RepositoryActionFailed);

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
                throw new BadRequestException(Messages.CountyInvalid);

            var city = await _repository
                .Query()
                .FirstOrDefaultAsync(c => c.Id == cityId && c.CountyId == countyId, cancellationToken);

            city!.Name = newName;

            await _repository.UpdateAsync(city, cancellationToken);
        }
    }
}
