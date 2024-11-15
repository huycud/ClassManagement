using FluentValidation;
using ClassManagement.Api.DTO.Department;

namespace ClassManagement.Api.Models.Validations.Departments
{
    public class CreateDepartmentValidator : AbstractValidator<CreateDepartmentRequest>
    {
        public CreateDepartmentValidator()
        {
            RuleFor(x => x.Id)

                .NotNull().NotEmpty().WithMessage("Id required.")

                .MaximumLength(20).WithMessage("Id must be less than 20 characters.")

                .Matches("^[a-zA-Z]+$").WithMessage("Id cannot be contain special characters.");


            RuleFor(x => x.Name)

                .NotNull().NotEmpty().WithMessage("Name required.")

                .MaximumLength(255).WithMessage("Name must be less than 255 characters.");
        }
    }
}
