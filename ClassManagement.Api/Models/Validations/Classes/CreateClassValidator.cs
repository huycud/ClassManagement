using ClassManagement.Api.DTO.Class;
using FluentValidation;
using Utilities.Common;

namespace ClassManagement.Api.Models.Validations.Classes
{
    public class CreateClassValidator : AbstractValidator<CreateClassRequest>
    {
        public CreateClassValidator()
        {
            RuleFor(x => x.Id)

                .NotNull().NotEmpty().WithMessage("Id required.")

                .MaximumLength(20).WithMessage("Id must be less than 20 characters.")

                .Matches(RegexConstants.CLASSID).WithMessage("Id can not be contain special characters.");

            RuleFor(x => x.Name)

                .NotNull().NotEmpty().WithMessage("Name required.")

                .MaximumLength(255).WithMessage("Name must be less than 255 characters.");

            RuleFor(x => x.ClassSize)

                .NotNull().NotEmpty().WithMessage("ClassSize required.")

                .GreaterThanOrEqualTo(30).WithMessage("ClassSize must be greater than or euqal 30.")

                .LessThanOrEqualTo(150).WithMessage("ClassSize must be less than or equal 150.");

            RuleFor(x => x.UserId)

                .NotNull().NotEmpty().WithMessage("UserId required.");

            RuleFor(x => x.SubjectId)

                .NotNull().NotEmpty().WithMessage("SubjectId required.")

                .MaximumLength(20).WithMessage("SubjectId must be less than 20 characters.")

                .Matches(RegexConstants.NAME).WithMessage("SubjectId cannot be contain special characters.");

            RuleFor(x => x.SemesterId)

                .NotNull().NotEmpty().WithMessage("SemesterId required.")

                .MaximumLength(20).WithMessage("SemesterId must be less than 20 characters.")

                .Matches(RegexConstants.NAME).WithMessage("SemesterId cannot be contain special characters.");

            RuleFor(x => x.Type)

                .IsInEnum().WithMessage("Type invalid.");

            RuleFor(x => x.DayOfWeek)

                .IsInEnum().WithMessage("DayOfWeek invalid.");

            RuleForEach(x => x.ClassPeriods)

                .IsInEnum().WithMessage("ClassPeriod invalid.");

            RuleFor(x => x.StartedAt)

                .NotNull().NotEmpty().WithMessage("StartedAt required.");

            RuleFor(x => x.EndedAt)

                .NotNull().NotEmpty().WithMessage("EndedAt required.")

                .GreaterThan(x => x.StartedAt).WithMessage("EndedAt must greater than StartedAt.");
        }
    }
}
