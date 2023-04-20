using Application.Interfaces;
using Common.Constants;
using Common.Exceptions;
using Contracts.Requests;
using Contracts.Responses;
using Data.Interfaces;
using Data.Models;

namespace Application.Services
{
    public class SubCategoryService : BaseService<SubCategory, int>, ISubCategoryService
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
                throw new NotFoundException(Messages.CategoryInvalid);

            var mapped = newSubCategoryRequest.ToModel(categoryId);

            var creationResult = await _repository.CreateAsync(mapped, cancellationToken);

            if (creationResult.RepositoryActionResult is not Data.Enums.RepositoryAction.Success)
                throw new InvalidOperationException(Messages.RepositoryActionFailed);

            return creationResult.CreatedEntity.ToDto();
        }
    }
}
