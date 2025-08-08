using FluentValidation;
using StockMode.Application.Features.Customers.Dtos;

namespace StockMode.Application.Features.Customers.Validators
{
    public class AddressDtoValidator : AbstractValidator<AddressDto>
    {
        public AddressDtoValidator()
        {
            RuleFor(a => a.Number)
                .NotEmpty().WithMessage("Number is required.")
                .NotEqual(0).WithMessage("Number must be greater than zero.");

            RuleFor(a => a.Street)
                .NotEmpty().WithMessage("Street is required.")
                .MaximumLength(200).WithMessage("Street cannot exceed 200 characters.");

            RuleFor(a => a.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(100).WithMessage("City cannot exceed 100 characters.");

            RuleFor(a => a.State)
                .NotEmpty().WithMessage("State is required.")
                .MaximumLength(50).WithMessage("State cannot exceed 50 characters.");

            RuleFor(a => a.ZipCode)
                .NotEmpty().WithMessage("ZipCode is required.")
                .MaximumLength(20).WithMessage("ZipCode cannot exceed 20 characters.");
        }
    }
}
