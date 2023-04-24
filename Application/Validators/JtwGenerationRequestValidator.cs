using Contracts.Requests;
using FluentValidation;

namespace Application.Validators
{
    public class JtwGenerationRequestValidator : AbstractValidator<JwtGenerationRequest>
    {
        public JtwGenerationRequestValidator()
        {
            RuleFor(j => j.UserId)
                .NotEmpty();

            RuleFor(j => j.Email)
                .EmailAddress()
                .NotEmpty();
        }
    }
}
