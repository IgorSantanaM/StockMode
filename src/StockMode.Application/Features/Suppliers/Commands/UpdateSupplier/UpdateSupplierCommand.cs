using MediatR;
using StockMode.Application.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.Suppliers.Commands.UpdateSupplier;

public record UpdateSupplierCommand(int Id, string Name, string CorporateName, string CNPJ, string ContatctPerson, string Email, string PhoneNumber, AddressDto AddressDto, string? Notes) : IRequest;
