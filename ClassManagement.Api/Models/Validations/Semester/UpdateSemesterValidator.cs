using FluentValidation;
using ClassManagement.Api.DTO.Semester;

namespace ClassManagement.Api.Models.Validations.Semester
{
    public class UpdateSemesterValidator : AbstractValidator<UpdateSemesterRequest>
    {
        public UpdateSemesterValidator()
        {
            RuleFor(x => x.Name)

                .NotNull().NotEmpty().WithMessage("Name required.")

                .MaximumLength(255).WithMessage("Name must be less than 255 characters.");
        }
    }
}
