using FluentValidation;
using StockMode.Application.Features.Products.Dtos;
using StockMode.Domain.Products;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.Products.Validators
{
    public class VariationCommandDtoValidator : AbstractValidator<VariationCommandDto>
    {
        public VariationCommandDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Variation name is required.");

            RuleFor(x => x.Sku)
                .NotEmpty().WithMessage("Variation SKU cannot be empty.")
                .Length(5, 12).WithMessage("Variation SKU needs to be betweem 5 and 12.");

            RuleFor(x => x.CostPrice)
                .GreaterThan(0).WithMessage("Cost price must be greater than zero.");

            RuleFor(x => x.SalePrice)
                .GreaterThan(0).WithMessage("Sale price must be greater than zero.")
                .GreaterThanOrEqualTo(x => x.CostPrice).WithMessage("Sale price must be greater than or equal to cost price.");

            RuleFor(x => x.StockQuantity)
                .GreaterThanOrEqualTo(0).WithMessage("Stock quantity cannot be negative.");
        }
    }
}
