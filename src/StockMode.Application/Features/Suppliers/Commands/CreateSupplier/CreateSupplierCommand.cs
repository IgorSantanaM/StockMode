using MediatR;
using StockMode.Application.Common.Dtos;

namespace StockMode.Application.Features.Suppliers.Commands.CreateSupplier;
public record CreateSupplierCommand(string Name, string CorporateName, string CNPJ, string ContatctPerson, string Email, string PhoneNumber, AddressDto AddressDto, string? Notes) : IRequest<int>;
