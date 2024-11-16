using FluentValidation;
using ClassManagement.Api.DTO.Users.Clients;
using Utilities.Common;

namespace ClassManagement.Api.Models.Validations.Users.Clients
{
    public class UpdateClientValidator : AbstractValidator<UpdateClientRequest>
    {
        public UpdateClientValidator()
        {
            RuleFor(x => x.Firstname)

                .NotNull().NotEmpty().WithMessage("Firstname required.")

                .Length(2, 20).WithMessage("Firstname must greater than 2 characters and less than 20 characters.")

                .Matches(RegexConstants.VIETNAMESENAME).WithMessage("Firstname must not contain special characters.");

            RuleFor(x => x.Lastname)

                .NotNull().NotEmpty().WithMessage("Lastname required.")

                .Length(2, 20).WithMessage("Lastname must greater than 2 characters and less than 20 characters.")

                .Matches(RegexConstants.VIETNAMESENAME).WithMessage("Lastname must not contain special characters.");

            RuleFor(x => x.DateOfBirth)

                .NotNull().NotEmpty().WithMessage("Birthday required.")

                .LessThan(DateTime.UtcNow).WithMessage("Birthday invalid.")

                .GreaterThan(DateTime.UtcNow.AddYears(-200)).WithMessage("Birthday invalid.");

            RuleFor(x => x.Address)

                .NotNull().NotEmpty().WithMessage("Address required.")

                .MaximumLength(255).WithMessage("Address must less than 255 characters.");
        }
    }
}
