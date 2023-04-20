using Contracts.Requests;
using Contracts.Responses;
using Data.Models;

namespace Application.Interfaces
{
    public interface IProductService : IService<Product, int>
    {
        Task<ProductResponse> CreateAsync(CreateProductRequest request, CancellationToken cancellationToken = default);
        Task<ProductResponse> FindById(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<ProductResponse>> GetAllFromCategory(int categoryId, CancellationToken cancellationToken = default);
        Task<IEnumerable<ProductResponse>> GetAllFromSubCategory(int subCategoryId, CancellationToken cancellationToken = default);
        Task Renew(int id, CancellationToken cancellationToken = default);
        Task UpdateAvailability(int id, ProductAvailability availability, CancellationToken cancellationToken = default);
    }
}
