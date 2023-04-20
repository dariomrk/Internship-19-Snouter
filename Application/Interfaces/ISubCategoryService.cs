using Contracts.Requests;
using Contracts.Responses;
using Data.Models;

namespace Application.Interfaces
{
    public interface ISubCategoryservice : IService<SubCategory, int>
    {
        Task<SubCategoryResponse> CreateAsync(int categoryId, CreateSubCategoryRequest newSubCategoryRequest, CancellationToken cancellationToken = default);
    }
}
