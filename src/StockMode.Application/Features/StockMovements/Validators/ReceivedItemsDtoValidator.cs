using FluentValidation;
using StockMode.Application.Features.StockMovements.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.StockMovements.Validators
{
    public class ReceivedItemsDtoValidator : AbstractValidator<ReceivedItemsDto>
    {
        public ReceivedItemsDtoValidator()
        {
            RuleFor(x => x.VariationId).GreaterThan(0);
            RuleFor(x => x.QuantityReceived).GreaterThan(0);
        }
    }
}
