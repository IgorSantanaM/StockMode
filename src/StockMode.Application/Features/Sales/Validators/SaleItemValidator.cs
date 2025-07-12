using FluentValidation;
using StockMode.Domain.Sales;

namespace StockMode.Application.Features.Sales.Validators
{
    public class SaleItemValidator : AbstractValidator<SaleItem>
    {
        public SaleItemValidator()
        {

        }
    }
}
