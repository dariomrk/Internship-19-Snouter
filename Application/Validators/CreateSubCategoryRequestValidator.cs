using Contracts.Requests;
using FluentValidation;

namespace Application.Validators
{
    public class CreateSubCategoryRequestValidator : AbstractValidator<CreateSubCategoryRequest>
    {
        public CreateSubCategoryRequestValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty();

            RuleFor(s => s.ValidationSchema)
                .NotEmpty();
        }
    }
}
