using FluentValidation;
using ClassManagement.Api.DTO.Class;
using Utilities.Common;

namespace ClassManagement.Api.Models.Validations.Classes
{
    public class UpdateClassValidator : AbstractValidator<UpdateClassRequest>
    {
        public UpdateClassValidator()
        {
            RuleFor(x => x.Name)

                .NotNull().NotEmpty().WithMessage("Name required")

                .MaximumLength(255).WithMessage("Name must be less than 255 characters.")

                .Matches(RegexConstants.VIETNAMESENAME).WithMessage("Name cannot be contain special characters.");

            RuleFor(x => x.ClassSize)

                .NotNull().NotEmpty().WithMessage("ClassSize required.")

                .GreaterThanOrEqualTo(30).WithMessage("ClassSize must be greater than or euqal 30.")

                .LessThanOrEqualTo(150).WithMessage("ClassSize must be less than or equal 150.");

            RuleFor(x => x.UserId)

                .NotNull().NotEmpty().WithMessage("UserId required.");

            RuleFor(x => x.StartedAt)

                .NotNull().NotEmpty().WithMessage("StartedAt required.");

            RuleFor(x => x.EndedAt)

                .NotNull().NotEmpty().WithMessage("EndedAt required.")

                .GreaterThan(x => x.StartedAt).WithMessage("EndedAt must greater than StartedAt.");
        }
    }
}
