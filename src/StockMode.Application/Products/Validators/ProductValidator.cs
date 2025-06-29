using FluentValidation;
using StockMode.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Products.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .Length(2, 100).WithMessage("Product name must be between 2 and 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.Variations)
                .NotEmpty().WithMessage("A product must have at least one variation.");
        }
    }
}
