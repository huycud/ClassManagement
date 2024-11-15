using FluentValidation;
using ClassManagement.Api.DTO.Users.Manager;
using Utilities.Common;

namespace ClassManagement.Api.Models.Validations.Users.Manager
{
    public class CreateAdminValidator : AbstractValidator<CreateAdminRequest>
    {
        public CreateAdminValidator()
        {
            RuleFor(x => x.Email)

                .NotNull().NotEmpty().WithMessage("Email required.")

                .Matches(RegexConstants.EMAIL).WithMessage("Email invalid.");

            RuleFor(x => x.UserName)

                .NotNull().NotEmpty().WithMessage("Username required.")

                .Length(5, 20).WithMessage("Username must be more than 5 characters and less than 20 characters.")

                .Matches(RegexConstants.NAME).WithMessage("Username cannot be contain special characters.");

            RuleFor(x => x.Password)

                .NotNull().NotEmpty().WithMessage("Password required.")

                .Length(6, 20).WithMessage("Password must be more than 6 characters and less than 20 characters.");

            RuleFor(x => x.ConfirmPassword)

                .Equal(x => x.Password).WithMessage("Passwords do not match.");

            RuleFor(x => x.Fullname)

                .NotNull().NotEmpty().WithMessage("Fullname required.")

                .Length(2, 255).WithMessage("Fullname must be more than 2 characters and less than 255 characters.")

                .Matches(RegexConstants.CLIENTNAME).WithMessage("Fullname cannot be contain special characters.");
        }
    }
}
