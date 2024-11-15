using FluentValidation;
using ClassManagement.Api.DTO.Department;

namespace ClassManagement.Api.Models.Validations.Departments
{
    public class UpdateDepartmentValidator : AbstractValidator<UpdateDepartmentRequest>
    {
        public UpdateDepartmentValidator()
        {
            RuleFor(x => x.Name)

                .NotNull().NotEmpty().WithMessage("Name required.")

                .MaximumLength(255).WithMessage("Name must be less than 255 characters.");
        }
    }
}
