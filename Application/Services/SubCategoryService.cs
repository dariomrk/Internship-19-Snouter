using Application.Interfaces;
using Common.Constants;
using Contracts.Requests;
using Contracts.Responses;
using Data.Interfaces;
using Data.Models;

namespace Application.Services
{
    public class SubCategoryService : BaseService<SubCategory, int>, ISubCategoryservice
    {
        private readonly IRepository<Category, int> _categoryRepository;

        public SubCategoryService(
            IRepository<SubCategory, int> repository,
            IRepository<Category, int> categoryRepository) : base(repository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<SubCategoryResponse> CreateAsync(
            int categoryId,
            CreateSubCategoryRequest newSubCategoryRequest,
            CancellationToken cancellationToken = default)
        {
            var category = _categoryRepository
                .FindAsync(categoryId, cancellationToken);

            if (category is null)
                throw new ArgumentException(Messages.CategoryInvalid);

            var mapped = newSubCategoryRequest.ToModel(categoryId);

            var creationResult = await _repository.CreateAsync(mapped, cancellationToken);

            if (creationResult.RepositoryActionResult is not Data.Enums.RepositoryAction.Success)
                throw new Exception(Messages.RepositoryActionFailed);

            return creationResult.CreatedEntity.ToDto();
        }
    }
}
