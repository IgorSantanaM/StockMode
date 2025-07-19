using FluentValidation;
using StockMode.Application.Features.Sales.Commands.CreateSale;

namespace StockMode.Application.Features.Sales.Validators
{
    public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleCommandValidator()
        {

            RuleFor(c => c.PaymentMethod)
            .IsInEnum()
            .WithMessage("A valid payment method must be specified.");
        }
    }
}
