using ClassManagement.Api.DTO.Authentication;
using FluentValidation;
using Utilities.Common;

namespace ClassManagement.Api.Models.Validations.Users
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordValidator()
        {
            RuleFor(x => x.Email)

                .NotNull().NotEmpty().WithMessage("Email required.")

                .Matches(RegexConstants.EMAIL).WithMessage("Email invalid.");

            RuleFor(x => x.NewPassword)

                .NotNull().NotEmpty().WithMessage("New Password required.")

                .Length(6, 20).WithMessage("New Password must greater than 6 characters and less than 20 characters.");

            RuleFor(x => x.ConfirmPassword)

                .Equal(x => x.NewPassword).WithMessage("Confirm Password do not match.");
        }
    }
}
