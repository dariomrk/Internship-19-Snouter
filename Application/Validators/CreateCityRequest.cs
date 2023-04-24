using Contracts.Requests;
using FluentValidation;

namespace Application.Validators
{
    public class CreateCityRequestValidator : AbstractValidator<CreateCityRequest>
    {
        public CreateCityRequestValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty();
        }
    }
}
