using Application.Interfaces;
using Common.Constants;
using Contracts.Requests;
using Contracts.Responses;
using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class UserService : BaseService<User, int>, IUserService
    {
        private readonly IRepository<City, int> _cityRepository;
        public UserService(
            IRepository<User, int> userRepository,
            IRepository<City, int> cityRepository
            ) : base(userRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<UserResponse> CreateAsync(CreateUserRequest newUserDetails, CancellationToken cancellationToken = default)
        {
            var mapped = newUserDetails.ToModel();

            var isInformationInUse = await _repository
                .Query()
                .AnyAsync(u => u.Username.Contains(mapped.Username)
                    || u.Email.Contains(mapped.Email)
                    || u.Phone.Contains(mapped.Phone),
                    cancellationToken);

            if (isInformationInUse)
                throw new InvalidOperationException(Messages.IdentityInformationInUse);

            var city = await _cityRepository
                .Query()
                .FirstOrDefaultAsync(c => newUserDetails.CityName.Contains(c.Name), cancellationToken);

            if (city is null)
                throw new InvalidOperationException(Messages.CityNotDefined);

            mapped.City = city;

            var creationResult = await _repository.CreateAsync(mapped, cancellationToken);

            if (creationResult.RepositoryActionResult is not Data.Enums.RepositoryAction.Success)
                throw new Exception(Messages.RepositoryActionFailed);

            return creationResult.CreatedEntity.ToDto();
        }
    }
}
