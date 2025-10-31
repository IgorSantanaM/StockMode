using FluentValidation;
using StockMode.Application.Features.Tags.Commands.CreateTag;

namespace StockMode.Application.Features.Tags.Validators
{
    public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
    {
        public CreateTagCommandValidator()
        {
            RuleFor(t => t.Name)
                .Length(2, 100).WithMessage("Tag name must be between 2 and 100 characters.");

            RuleFor(t => t.Color)
                .MaximumLength(20).WithMessage("Tag color cannot exceed 20 characters.");

            RuleFor(t => t)
                .Must((command) => !(string.IsNullOrWhiteSpace(command.Name) && string.IsNullOrWhiteSpace(command.Color)))
                .WithMessage("Tag must have at least a name or a color.");
        }
    }
}
