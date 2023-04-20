using Contracts.Requests;
using Contracts.Responses;
using Data.Models;

namespace Application.Interfaces
{
    public interface ICategoryService : IService<Category, int>
    {
        Task<CategoryResponse> CreateAsync(CreateCategoryRequest newCategory, CancellationToken cancellationToken = default);
    }
}
