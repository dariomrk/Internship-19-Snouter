using Application.Interfaces;
using Common.Constants;
using Contracts.Requests;
using Contracts.Responses;
using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class ProductService : BaseService<Product, int>, IProductService
    {
        private readonly IRepository<City, int> _cityRepository;
        private readonly IRepository<Country, int> _countryRepository;
        private readonly IRepository<County, int> _countyRepository;

        public ProductService(
            IRepository<Product, int> productRepository,
            IRepository<City, int> cityRepository,
            IRepository<Country, int> countryRepository,
            IRepository<County, int> countyRepository) : base(productRepository)
        {
            _cityRepository = cityRepository;
            _countryRepository = countryRepository;
            _countyRepository = countyRepository;
        }

        public async Task<ProductResponse> CreateAsync(
            CreateProductRequest request,
            CancellationToken cancellationToken = default)
        {
            var city = await _cityRepository.FindAsync(request.CityId, cancellationToken);

            if (city is null)
                throw new ArgumentNullException(Messages.CityNotDefined);

            var mapped = request.ToModel();

            var creationResult = await _repository.CreateAsync(mapped, cancellationToken);

            if (creationResult.RepositoryActionResult is not Data.Enums.RepositoryAction.Success)
                throw new InvalidOperationException(Messages.RepositoryActionFailed);

            var result = await _repository
                .Query()
                .AsNoTracking()
                .Include(p => p.Currency)
                .Include(p => p.PreciseLocation)
                .Include(p => p.City).ThenInclude(p => p.County).ThenInclude(p => p.Country)
                .Include(p => p.SubCategory).ThenInclude(p => p.Category)
                .Include(p => p.Creator)
                .FirstAsync(p => p.Id == creationResult.CreatedEntity.Id);

            return result.ToDto();
        }

        public async Task<IEnumerable<ProductResponse>> GetAllFromCategory(
            int categoryId,
            CancellationToken cancellationToken = default)
        {
            var result = await _repository
                .Query()
                .AsNoTracking()
                .Include(p => p.Currency)
                .Include(p => p.PreciseLocation)
                .Include(p => p.City).ThenInclude(p => p.County).ThenInclude(p => p.Country)
                .Include(p => p.SubCategory).ThenInclude(p => p.Category)
                .Include(p => p.Creator)
                .Where(p => p.SubCategory.Category.Id == categoryId)
                .Select(p => p.ToDto())
                .ToListAsync(cancellationToken);

            return result;
        }

        public async Task<IEnumerable<ProductResponse>> GetAllFromSubCategory(
            int subCategoryId,
            CancellationToken cancellationToken = default)
        {
            var result = await _repository
                .Query()
                .AsNoTracking()
                .Include(p => p.Currency)
                .Include(p => p.PreciseLocation)
                .Include(p => p.City).ThenInclude(p => p.County).ThenInclude(p => p.Country)
                .Include(p => p.SubCategory).ThenInclude(p => p.Category)
                .Include(p => p.Creator)
                .Where(p => p.SubCategory.Id == subCategoryId)
                .Select(p => p.ToDto())
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}
