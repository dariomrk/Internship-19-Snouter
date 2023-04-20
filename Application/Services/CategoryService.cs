using Application.Interfaces;
using Common.Constants;
using Contracts.Requests;
using Contracts.Responses;
using Data.Interfaces;
using Data.Models;

namespace Application.Services
{
    public class CategoryService : BaseService<Category, int>, ICategoryService
    {
        public CategoryService(IRepository<Category, int> repository) : base(repository) { }

        public async Task<CategoryResponse> CreateAsync(
            CreateCategoryRequest newCategoryRequest,
            CancellationToken cancellationToken = default)
        {
            var mapped = newCategoryRequest.ToModel();

            var creationResult = await _repository.CreateAsync(mapped, cancellationToken);

            if (creationResult.RepositoryActionResult is not Data.Enums.RepositoryAction.Success)
                throw new Exception(Messages.RepositoryActionFailed);

            return creationResult.CreatedEntity.ToDto();
        }
    }
}
