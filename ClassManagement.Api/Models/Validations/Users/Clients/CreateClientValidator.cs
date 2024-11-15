using FluentValidation;
using ClassManagement.Api.DTO.Users.Clients;
using Utilities.Common;

namespace ClassManagement.Api.Models.Validations.Users.Clients
{
    public class CreateClientValidator : AbstractValidator<CreateClientRequest>
    {
        public CreateClientValidator()
        {
            RuleFor(x => x.RoleName)

               .NotEmpty().NotEmpty().WithMessage("RoleName required.")

               .Length(2, 255).WithMessage("RoleName must be more than 2 characters and less than 255 characters.");

            RuleFor(x => x.UserName)

                .NotEmpty().NotEmpty().WithMessage("Username required.")

                .Length(5, 20).WithMessage("Username must be more than 5 characters and less than 20 characters.")

                .Matches(RegexConstants.NAME).WithMessage("Username cannot be contain special characters.");

            RuleFor(x => x.Password)

                .NotNull().NotEmpty().WithMessage("Password required.")

                .Length(6, 20).WithMessage("Password must be more than 6 characters and less than 20 characters.");

            RuleFor(x => x.ConfirmPassword)

                .Equal(x => x.Password).WithMessage("Passwords do not match.");

            RuleFor(x => x.Firstname)

                .NotNull().NotEmpty().WithMessage("Firstname required.")

                .Length(2, 255).WithMessage("Firstname must be more than 2 characters and less than 255 characters.")

                .Matches(RegexConstants.CLIENTNAME).WithMessage("Firstname cannot be contain special characters.");

            RuleFor(x => x.Lastname)

                .NotNull().NotEmpty().WithMessage("Lastname required.")

                .Length(2, 255).WithMessage("Lastname must be more than 2 characters and less than 255 characters.")

                .Matches(RegexConstants.CLIENTNAME).WithMessage("Lastname cannot be contain special characters.");

            RuleFor(x => x.Email)

                .NotNull().NotEmpty().WithMessage("Email required.")

                .Matches(RegexConstants.EMAIL).WithMessage("Email invalid.");

            RuleFor(x => x.Address)

                .NotNull().NotEmpty().WithMessage("Address required.")

                .MaximumLength(255).WithMessage("Address must be less than 255 characters.");

            RuleFor(x => x.Gender)

                .IsInEnum().WithMessage("Gender invalid.");

            RuleFor(x => x.DateOfBirth)

                .NotNull().NotEmpty().WithMessage("Birthday required.")

                .LessThan(x => DateTime.UtcNow).WithMessage("Birthday invalid.")

                .GreaterThan(DateTime.UtcNow.AddYears(-200)).WithMessage("Birthday invalid.");

            RuleFor(x => x.DepartmentId)

                .NotNull().NotEmpty().WithMessage("DepartmentId required.")

                .MaximumLength(20).WithMessage("DepartmentId must be less than 20 characters.")

                .Matches(RegexConstants.NAME).WithMessage("DepartmentId cannot be contain special characters.");
        }
    }
}
