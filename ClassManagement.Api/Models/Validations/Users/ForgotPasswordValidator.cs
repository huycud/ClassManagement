using ClassManagement.Api.DTO.Authentication;
using FluentValidation;
using Utilities.Common;

namespace ClassManagement.Api.Models.Validations.Users
{
    public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordRequest>
    {
        public ForgotPasswordValidator()
        {
            RuleFor(x => x.Email)

                .NotNull().NotEmpty().WithMessage("Email required.")

                .Matches(RegexConstants.EMAIL).WithMessage("Email invalid.");
        }
    }
}
