using FluentValidation;
using ClassManagement.Api.DTO.AppRole;
using Utilities.Common;

namespace ClassManagement.Api.Models.Validations.AppRole
{
    public class CreateRoleValidator : AbstractValidator<CreateRoleRequest>
    {
        public CreateRoleValidator()
        {
            RuleFor(x => x.Name)

                .NotNull().NotEmpty().WithMessage("Name required.")

                .Length(2, 255).WithMessage("Name must be more than 2 characters and less than 255 characters.")

                .Matches(RegexConstants.NAME).WithMessage("Name cannot be contain special characters.");
        }
    }
}
