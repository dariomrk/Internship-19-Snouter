using Contracts.Requests;
using FluentValidation;

namespace Application.Validators
{
    public class UserRequestValidator : AbstractValidator<UserRequest>
    {
        public UserRequestValidator()
        {
            RuleFor(u => u.FirstName)
                .NotEmpty();

            RuleFor(u => u.LastName)
                .NotEmpty();

            RuleFor(u => u.Email)
                .EmailAddress();

            RuleFor(u => u.Phone)
                .Length(10)
                .NotEmpty();

            RuleFor(u => u.CityName)
                .NotEmpty();

            RuleFor(u => u.CountyName)
                .NotEmpty();

            RuleFor(u => u.CountryName)
                .NotEmpty();

            RuleFor(u => u.Latitude)
                .InclusiveBetween(-90, 90)
                .When(u => u.Latitude is not null);

            RuleFor(u => u.Longitude)
                .InclusiveBetween(-180, 180)
                .When(u => u.Longitude is not null);
        }
    }
}
