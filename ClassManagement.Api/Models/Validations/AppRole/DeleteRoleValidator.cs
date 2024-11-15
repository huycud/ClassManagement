using FluentValidation;
using ClassManagement.Api.DTO.AppRole;
using Utilities.Common;

namespace ClassManagement.Api.Models.Validations.AppRole
{
    public class DeleteRoleValidator : AbstractValidator<DeleteRoleRequest>
    {
        public DeleteRoleValidator()
        {
            RuleFor(x => x.Name)

                .NotNull().NotEmpty().WithMessage("Name required.")

                .Length(2, 200).WithMessage("Name must be more than 2 characters and less than 20 characters.")

                .Matches(RegexConstants.NAME).WithMessage("Name cannot be contain special characters.");
        }
    }
}
