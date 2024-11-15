using FluentValidation;
using ClassManagement.Api.DTO.Subject;

namespace ClassManagement.Api.Models.Validations.Subjects
{
    public class UpdateSubjectValidator : AbstractValidator<UpdateSubjectRequest>
    {
        public UpdateSubjectValidator()
        {
            RuleFor(x => x.Name)

                .NotNull().NotEmpty().WithMessage("Name required")

                .MaximumLength(255).WithMessage("Name must be less than 255 characters.");

            RuleFor(x => x.Status)

                .IsInEnum().WithMessage("Status invalid.");

            RuleFor(x => x.Credit)

                .NotNull().NotEmpty().WithMessage("Credit required")

                .GreaterThan(0).WithMessage("Credit must be greater than 0.")

                .LessThanOrEqualTo(10).WithMessage("Credit must be less than or equal 10.");
        }
    }
}
