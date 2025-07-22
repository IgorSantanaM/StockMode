using FluentValidation;
using StockMode.Application.Features.StockMovements.Commands.ReceivePurchaseOrder;

namespace StockMode.Application.Features.StockMovements.Validators
{
    public class ReceivePurchaseOrderCommandValidator : AbstractValidator<ReceivePurchaseOrderCommand>
    {
        public ReceivePurchaseOrderCommandValidator()
        {
            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("The purchase order should have at least one item.");

            RuleForEach(x => x.Items).SetValidator(new ReceivedItemsDtoValidator());
        }
    }
}
