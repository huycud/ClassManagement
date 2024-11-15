using FluentValidation;
using ClassManagement.Api.DTO.Subject;
using Utilities.Common;

namespace ClassManagement.Api.Models.Validations.Subjects
{
    public class CreateSubjectValidator : AbstractValidator<CreateSubjectRequest>
    {
        public CreateSubjectValidator()
        {
            RuleFor(x => x.Id)

               .NotNull().NotEmpty().WithMessage("Id required.")

               .MaximumLength(20).WithMessage("Id must be less than 20 characters.")

               .Matches(RegexConstants.NAME).WithMessage("Id cannot be contain special characters.");

            RuleFor(x => x.Name)

                .NotNull().NotEmpty().WithMessage("Name required")

                .MaximumLength(255).WithMessage("Name must be less than 255 characters.");

            RuleFor(x => x.Status)

                .IsInEnum().WithMessage("Status invalid.");

            RuleFor(x => x.Credit)

                .NotNull().NotEmpty().WithMessage("Credit required")

                .GreaterThan(0).WithMessage("Credit must be greater than 0.")

                .LessThanOrEqualTo(10).WithMessage("Credit must be less than or equal 10.");

            RuleFor(x => x.DepartmentId)

               .NotNull().NotEmpty().WithMessage("DepartmentId required.")

               .MaximumLength(20).WithMessage("DepartmentId must be less than 20 characters.")

               .Matches(RegexConstants.NAME).WithMessage("DepartmentId cannot be contain special characters.");
        }
    }
}
