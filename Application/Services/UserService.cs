using Application.Interfaces;
using Common.Constants;
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

        public async Task<UserResponse> CreateAsync(UserRequest newUserDetails, CancellationToken cancellationToken = default)
        {
            var mapped = newUserDetails.ToModel();

            var isInformationInUse = await _repository
                .Query()
                .AnyAsync(u => u.Username.Contains(mapped.Username)
                    || u.Email.Contains(mapped.Email)
                    || u.Phone.Contains(mapped.Phone),
                    cancellationToken);

            if (isInformationInUse)
                throw new InvalidOperationException(Messages.IdentityInformationInUse); // TODO use custom exception -> catch in middleware

            try
            {
                var country = await _countryRepository
                    .Query()
                    .AsNoTracking()
                    .FirstAsync(c => c.Name == mapped.City.County.Country.Name);

                var county = await _countyRepository
                    .Query()
                    .AsNoTracking()
                    .FirstAsync(c => c.Name == mapped.City.County.Name);

                var city = await _cityRepository
                    .Query()
                    .AsNoTracking()
                    .FirstAsync(c => c.Name == mapped.City.Name);
            }
            catch (Exception)
            {
                throw new InvalidOperationException(Messages.CityNotDefined);  // TODO use custom exception -> catch in middleware
            }

            var creationResult = await _repository.CreateAsync(mapped, cancellationToken);

            if (creationResult.RepositoryActionResult is not Data.Enums.RepositoryAction.Success)
                throw new Exception(Messages.RepositoryActionFailed);  // TODO use custom exception -> catch in middleware

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
                throw new InvalidOperationException();

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
                    throw new InvalidOperationException(Messages.IdentityInformationInUse);

                var country = await _countryRepository
                    .Query()
                    .AsNoTracking()
                    .FirstAsync(c => c.Name == mapped.City.County.Country.Name);

                var county = await _countyRepository
                    .Query()
                    .AsNoTracking()
                    .FirstAsync(c => c.Name == mapped.City.County.Name);

                var city = await _cityRepository
                    .Query()
                    .AsNoTracking()
                    .FirstAsync(c => c.Name == mapped.City.Name);

                currentUser.FirstName = mapped.FirstName;
                currentUser.LastName = mapped.LastName;
                currentUser.Email = mapped.Email;
                currentUser.Phone = mapped.Phone;
                currentUser.Username = mapped.Username;
                currentUser.City = mapped.City;
                currentUser.PreciseLocation = mapped.PreciseLocation;

                var updateResult = await _repository.UpdateAsync(currentUser, cancellationToken);

                if (updateResult is not RepositoryAction.Success)
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
