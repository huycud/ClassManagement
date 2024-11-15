using FluentValidation;
using ClassManagement.Api.DTO.Notifies;

namespace ClassManagement.Api.Models.Validations.Notifies
{
    public class UpdateNotifyValidator : AbstractValidator<UpdateNotifyRequest>
    {
        public UpdateNotifyValidator()
        {
            RuleFor(x => x.Title)

                .NotNull().NotEmpty().WithMessage("Title required.")

                .MaximumLength(255).WithMessage("Title must be less than 255 characters.");

            RuleFor(x => x.Content)

                .NotNull().NotEmpty().WithMessage("Content required.");

            RuleFor(x => x.Type)

                .IsInEnum().WithMessage("Type invalid.");
        }
    }
}
