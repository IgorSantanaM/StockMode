using FluentValidation;
using StockMode.Application.Common.Validators;
using StockMode.Application.Features.Customers.Commands.CreateCustomer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.Customers.Validators
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Customer name is required.")
                .Length(2, 150).WithMessage("Customer name must be between 2 and 150 characters.");

            RuleFor(c => c.Email)
               .NotEmpty().WithMessage("Email is required.")
               .EmailAddress().WithMessage("Invalid email format.")
               .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");

            RuleFor(c => c.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\(\d{2}\) \d{4,5}-\d{4}$").WithMessage("Phone number must be in the format (XX) XXXX-XXXX or (XX) XXXXX-XXXX.");

            RuleFor(c => c.AddressDto)
                .NotNull().WithMessage("Address is required.")
                .SetValidator(new AddressDtoValidator());

            RuleFor(c => c.Notes)
                .MaximumLength(500).WithMessage("Notes cannot exceed 500 characters.");
        }
    }
}
