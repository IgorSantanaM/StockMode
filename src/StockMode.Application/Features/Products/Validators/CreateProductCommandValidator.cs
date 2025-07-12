using FluentValidation;
using StockMode.Application.Features.Products.Commands.CreateProduct;

namespace StockMode.Application.Features.Products.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .Length(2, 100).WithMessage("Product name must be between 2 and 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.Variations)
                .NotEmpty().WithMessage("A product must have at least one variation.");

            RuleForEach(x => x.Variations)
                .SetValidator(new VariationCommandDtoValidator())
                .WithMessage("One or more variations are invalid.");
        }
    }
}
