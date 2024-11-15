using FluentValidation;
using ClassManagement.Api.DTO.Semester;
using Utilities.Common;

namespace ClassManagement.Api.Models.Validations.Semester
{
    public class CreateSemesterValidator : AbstractValidator<CreateSemesterRequest>
    {
        public CreateSemesterValidator()
        {
            RuleFor(x => x.Id)

                .NotNull().NotEmpty().WithMessage("Id required.")

                .MaximumLength(20).WithMessage("Id must be less than 20 characters.")

                .Matches(RegexConstants.NAME).WithMessage("Id cannot be contain special characters.");

            RuleFor(x => x.Name)

                .NotNull().NotEmpty().WithMessage("Name required.")

                .MaximumLength(255).WithMessage("Name must be less than 255 characters.");
        }
    }
}
