using FluentValidation;
using StockMode.Application.Features.Sales.Dtos;
using StockMode.Domain.Sales;
using System.Xml;

namespace StockMode.Application.Features.Sales.Validators
{
    public class AddItemToSaleCommandDtoValidator : AbstractValidator<AddItemToSaleCommandDto>
    {
        public AddItemToSaleCommandDtoValidator()
        {
            RuleFor(si => si.SaleId)
                .NotEmpty()
                .WithMessage("Variation ID cannot be empty.");

            RuleFor(si => si.VariationId)
                .NotEmpty()
                .WithMessage("Variation ID cannot be empty.");

            RuleFor(si => si.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than zero.");

            RuleFor(si => si.PriceAtSale)
                .GreaterThan(0)
                .WithMessage("Price at sale must be greater than zero.");
        }
    }
}
