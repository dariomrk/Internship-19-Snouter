using Application.Interfaces;
using Common.Constants;
using Contracts.Requests;
using Contracts.Responses;
using Data.Interfaces;
using Data.Models;
using FluentValidation;

namespace Application.Services
{
    public class CategoryService : BaseService<Category, int>, ICategoryService
    {
        private readonly IValidator<CreateCategoryRequest> _createCategoryRequestValidator;
        public CategoryService(IRepository<Category, int> repository, IValidator<CreateCategoryRequest> categoryValidator) : base(repository)
        {
            _createCategoryRequestValidator = categoryValidator;
        }

        public async Task<CategoryResponse> CreateAsync(
            CreateCategoryRequest newCategoryRequest,
            CancellationToken cancellationToken = default)
        {
            await _createCategoryRequestValidator.ValidateAndThrowAsync(newCategoryRequest, cancellationToken);

            var mapped = newCategoryRequest.ToModel();

            var creationResult = await _repository.CreateAsync(mapped, cancellationToken);

            if (creationResult.RepositoryActionResult is not Data.Enums.RepositoryAction.Success)
                throw new InvalidOperationException(Messages.RepositoryActionFailed);

            return creationResult.CreatedEntity.ToDto();
        }
    }
}
