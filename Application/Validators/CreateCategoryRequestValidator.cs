using Common.Constants;
using Contracts.Requests;
using Data.Interfaces;
using Data.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Validators
{
    public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
    {
        private readonly IRepository<Category, int> _categoryRepository;

        public CreateCategoryRequestValidator(IRepository<Category, int> categoryRepository)
        {
            _categoryRepository = categoryRepository;

            RuleFor(c => c.Name)
                .NotEmpty()
                .MustAsync(async (name, cancellationToken) =>
                {
                    var nameTaken = await _categoryRepository
                        .Query()
                        .AnyAsync(c => c.Name == name);

                    return !nameTaken;
                })
                .WithMessage(Messages.NameTaken);
        }
    }
}
