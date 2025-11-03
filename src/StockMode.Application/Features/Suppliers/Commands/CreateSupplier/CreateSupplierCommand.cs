using MediatR;
using StockMode.Application.Common.Dtos;

namespace StockMode.Application.Features.Suppliers.Commands.CreateSupplier;
public record CreateSupplierCommand(string Name, string CorporateName, string CNPJ, string ContactPerson, string Email, string PhoneNumber, AddressDto AddressDto, string? Notes, IReadOnlyCollection<int>? TagIds) : IRequest<int>;
