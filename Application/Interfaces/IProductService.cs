using Contracts.Responses;
using Data.Models;

namespace Application.Interfaces
{
    public interface IProductService : IService<Product, int>
    {
        Task<IEnumerable<ProductResponse>> GetAllFromCategory(int categoryId, CancellationToken cancellationToken = default);
        Task<IEnumerable<ProductResponse>> GetAllFromSubCategory(int subCategoryId, CancellationToken cancellationToken = default);
    }
}
