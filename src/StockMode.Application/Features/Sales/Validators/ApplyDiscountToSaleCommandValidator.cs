using FluentValidation;
using FluentValidation.Validators;
using StockMode.Application.Features.Sales.Commands.ApplyDiscountToSale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.Sales.Validators
{
    public class ApplyDiscountToSaleCommandValidator : AbstractValidator<ApplyDiscountToSaleCommand>
    {
        public ApplyDiscountToSaleCommandValidator()
        {
            RuleFor(d => d.SaleId)
                .GreaterThan(0).WithMessage("Sale ID must be greater a valid number.");

            RuleFor(d => d.DiscountAmount)
                .GreaterThanOrEqualTo(0).WithMessage("The discount amount cannot be negative.");
        }
    }
}
