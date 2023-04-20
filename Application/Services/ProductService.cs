using Application.Interfaces;
using Contracts.Responses;
using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class ProductService : BaseService<Product, int>, IProductService
    {
        public ProductService(IRepository<Product, int> repository) : base(repository) { }

        public async Task<IEnumerable<ProductResponse>> GetAllFromCategory(
            int categoryId,
            CancellationToken cancellationToken = default)
        {
            var result = await _repository
                .Query()
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
