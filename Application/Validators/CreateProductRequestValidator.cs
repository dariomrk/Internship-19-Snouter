using Contracts.Requests;
using FluentValidation;

namespace Application.Validators
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductRequestValidator()
        {

            RuleFor(p => p.Name)
                .NotEmpty();

            RuleFor(p => p.Description)
                .NotEmpty();

            RuleFor(p => p.Price)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(p => p.Properties)
                .NotEmpty();

            RuleFor(p => p.CityId)
                .NotEmpty()
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.CurrencyId)
                .NotEmpty()
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.CreatorId)
                .NotEmpty()
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.Price)
                .GreaterThan(0);

            RuleFor(p => p.Latitude)
                .InclusiveBetween(-90, 90)
                .When(p => p.Latitude is not null);

            RuleFor(p => p.Longitude)
                .InclusiveBetween(-180, 180)
                .When(p => p.Longitude is not null);

            RuleFor(p => p.State)
                .NotEmpty();

            RuleFor(p => p.SubCategoryId)
                .NotEmpty()
                .GreaterThanOrEqualTo(0);
        }
    }
}
