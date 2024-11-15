using FluentValidation;
using ClassManagement.Api.DTO.Users;

namespace ClassManagement.Api.Models.Validations.Users
{
    public class UpdatePasswordValidator : AbstractValidator<UpdatePasswordRequest>
    {
        public UpdatePasswordValidator()
        {
            RuleFor(x => x.CurrentPassword)

                .NotNull().NotEmpty().WithMessage("Password required.");

            RuleFor(x => x.NewPassword)

                .NotNull().NotEmpty().WithMessage("New Password required.")

                .Length(6, 20).WithMessage("New Password must greater than 6 characters and less than 20 characters.");

            RuleFor(x => x.ConfirmPassword)

                .Equal(x => x.NewPassword).WithMessage("Confirm Password do not match.");
        }
    }
}
