using Application.Interfaces;
using Common.Constants;
using Common.Extensions;
using Contracts.Requests;
using Contracts.Responses;
using Data.Enums;
using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class UserService : BaseService<User, int>, IUserService
    {
        private readonly IRepository<City, int> _cityRepository;
        private readonly IRepository<County, int> _countyRepository;
        private readonly IRepository<Country, int> _countryRepository;

        public UserService(
            IRepository<User, int> userRepository,
            IRepository<City, int> cityRepository,
            IRepository<County, int> countyRepository,
            IRepository<Country, int> countryRepository
            ) : base(userRepository)
        {
            _cityRepository = cityRepository;
            _countyRepository = countyRepository;
            _countryRepository = countryRepository;
        }

        public async Task<UserResponse> CreateAsync(UserRequest newUserRequest, CancellationToken cancellationToken = default)
        {
            var mapped = newUserRequest.ToModel();

            var isInformationInUse = await _repository
                .Query()
                .AnyAsync(u => u.Username.Contains(mapped.Username)
                    || u.Email.Contains(mapped.Email)
                    || u.Phone.Contains(mapped.Phone),
                    cancellationToken);

            if (isInformationInUse)
                throw new ArgumentException(Messages.IdentityInformationInUse);

            try
            {
                var country = await _countryRepository
                    .Query()
                    .AsNoTracking()
                    .FirstAsync(c => c.Name.ToLower() == newUserRequest.CountryName
                        .Trim()
                        .ToLower()
                        .Normalize());

                var county = await _countyRepository
                    .Query()
                    .AsNoTracking()
                    .FirstAsync(c => c.Name.ToLower() == newUserRequest.CountyName
                        .Trim()
                        .ToLower()
                        .Normalize());

                var city = await _cityRepository
                    .Query()
                    .AsNoTracking()
                    .FirstAsync(c => c.Name.ToLower() == newUserRequest.CityName
                        .Trim()
                        .ToLower()
                        .Normalize());

                mapped.CityId = city.Id;
            }
            catch (Exception)
            {
                throw new ArgumentException(Messages.CityNotDefined);
            }

            var creationResult = await _repository.CreateAsync(mapped, cancellationToken);

            if (creationResult.RepositoryActionResult is not RepositoryAction.Success)
                throw new Exception(Messages.RepositoryActionFailed);

            var createdUser = await _repository
                .Query()
                .AsNoTracking()
                .Where(u => u.Id == creationResult.CreatedEntity.Id)
                .Include(u => u.PreciseLocation)
                .Include(u => u.City)
                    .ThenInclude(c => c.County)
                    .ThenInclude(c => c.Country)
                .FirstAsync();

            return createdUser.ToDto();
        }

        public override async Task<ICollection<User>> GetAll(CancellationToken cancellationToken = default)
        {
            var users = await _repository
                .Query()
                .AsNoTracking()
                .Include(u => u.PreciseLocation)
                .Include(u => u.City).ThenInclude(u => u.County).ThenInclude(u => u.Country)
                .ToListAsync();

            return users;
        }

        public override async Task<User?> FindAsync(int id, CancellationToken cancellationToken = default)
        {
            var user = await _repository
                .Query()
                .Include(u => u.Products)
                .Include(u => u.PreciseLocation)
                .Include(u => u.City)
                    .ThenInclude(c => c.County)
                    .ThenInclude(c => c.Country)
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

            return user;

        }

        public async Task<UserResponse> UpdateAsync(int id, UserRequest updateUserDetails, CancellationToken cancellationToken = default)
        {

            var currentUser = await _repository.FindAsync(id, cancellationToken);

            if (currentUser is null)
                throw new ArgumentException();

            await _repository.BeginTransactionAsync(cancellationToken);

            var mapped = updateUserDetails.ToModel();

            try
            {
                var informationInUse = await _repository
                    .Query()
                    .Where(u => u.Id != id)
                    .AnyAsync(u => u.Username.Contains(mapped.Username)
                        || u.Email.Contains(mapped.Email)
                        || u.Phone.Contains(mapped.Phone),
                        cancellationToken);

                if (informationInUse)
                    throw new ArgumentException(Messages.IdentityInformationInUse);

                var country = await _countryRepository
                    .Query()
                    .AsNoTracking()
                    .FirstAsync(c => c.Name.ToLower() == updateUserDetails.CountryName.Sanitize());

                var county = await _countyRepository
                    .Query()
                    .AsNoTracking()
                    .FirstAsync(c => c.Name.ToLower() == updateUserDetails.CountyName.Sanitize());

                var city = await _cityRepository
                    .Query()
                    .AsNoTracking()
                    .FirstAsync(c => c.Name.ToLower() == updateUserDetails.CityName.Sanitize());

                currentUser.FirstName = mapped.FirstName;
                currentUser.LastName = mapped.LastName;
                currentUser.Email = mapped.Email;
                currentUser.Phone = mapped.Phone;
                currentUser.Username = mapped.Username;
                currentUser.CityId = city.Id;
                currentUser.PreciseLocation = updateUserDetails.Latitude.HasValue && updateUserDetails.Longitude.HasValue
                    ? new PreciseLocation
                    {
                        Latitude = updateUserDetails.Latitude.Value,
                        Longitude = updateUserDetails.Longitude.Value
                    }
                    : null;

                var updateResult = await _repository.UpdateAsync(currentUser, cancellationToken);

                if (updateResult is not RepositoryAction.Success or RepositoryAction.NoChanges)
                    throw new Exception(Messages.RepositoryActionFailed);
            }
            catch (Exception)
            {
                await _repository.RollbackTransactionAsync(cancellationToken);
                throw new InvalidOperationException(Messages.UpdateFailed);
            }

            await _repository.CommitTransactionAsync(cancellationToken);
            return mapped.ToDto();
        }
    }
}
