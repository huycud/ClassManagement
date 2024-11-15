using FluentValidation;
using ClassManagement.Api.DTO.AppRole;
using Utilities.Common;

namespace ClassManagement.Api.Models.Validations.AppRole
{
    public class AddRoleToUserValidator : AbstractValidator<RoleRequest>
    {
        public AddRoleToUserValidator()
        {
            RuleFor(x => x.RoleName)

                .NotNull().NotEmpty().WithMessage("Name required.")

                .Length(2, 200).WithMessage("Name must greater than 2 characters and less than 20 characters.")

                .Matches(RegexConstants.NAME).WithMessage("Name must not contain special characters.");
        }
    }
}
