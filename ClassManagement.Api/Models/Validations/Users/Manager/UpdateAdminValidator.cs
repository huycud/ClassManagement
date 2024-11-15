using FluentValidation;
using ClassManagement.Api.DTO.Users.Manager;
using Utilities.Common;

namespace ClassManagement.Api.Models.Validations.Users.Manager
{
    public class UpdateAdminValidator : AbstractValidator<UpdateAdminRequest>
    {
        public UpdateAdminValidator()
        {
            RuleFor(x => x.Fullname)

                .NotEmpty().NotEmpty().WithMessage("Fullname required.")

                .Length(2, 255).WithMessage("FirstName must greater than 2 characters and less than 255 characters.")

                .Matches(RegexConstants.CLIENTNAME).WithMessage("Fullname must not contain special characters.");
        }
    }
}
