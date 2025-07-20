using FluentValidation;
using StockMode.Application.Features.Sales.Commands.CreateSaleItem;
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

        }
    }
}
