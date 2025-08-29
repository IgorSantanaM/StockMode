using FluentValidation;
using StockMode.Application.Common.Validators;
using StockMode.Application.Features.Suppliers.Commands.CreateSupplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.Suppliers.Validators
{
    public class CreateSupplierCommandValidator : AbstractValidator<CreateSupplierCommand>
    {
        public CreateSupplierCommandValidator()
        {
            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(command => command.CorporateName)
                .NotEmpty().WithMessage("Corporate name is required.")
                .MaximumLength(200).WithMessage("Corporate name cannot exceed 200 characters.");

            RuleFor(command => command.CNPJ)
                .NotEmpty().WithMessage("CNPJ is required.")
                .Matches(@"^\d{2}\.\d{3}\.\d{3}\/\d{4}-\d{2}$").WithMessage("CNPJ must be in the format XX.XXX.XXX/XXXX-XX.");

            RuleFor(command => command.ContactPerson)
                .NotEmpty().WithMessage("Contact person is required.")
                .MaximumLength(100).WithMessage("Contact person cannot exceed 100 characters.");

            RuleFor(command => command.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");

            RuleFor(command => command.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\(\d{2}\) \d{4,5}-\d{4}$").WithMessage("Phone number must be in the format (XX) XXXX-XXXX or (XX) XXXXX-XXXX.");

            RuleFor(command => command.AddressDto)
                .SetValidator(new AddressDtoValidator())
                .WithMessage("Invalid address details.");
        }
    }
}
