using FluentValidation;
using StockMode.Domain.StockMovements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.StockMovements.Validators
{
    public class StockMovementValidator : AbstractValidator<StockMovement>
    {
        public StockMovementValidator()
        {

        }
    }
}
