using Org.BouncyCastle.Asn1;
using StockMode.Application.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.Suppliers.Dtos
{
    public record SupplierDetailsDto(int Id, string Name, string CorporateName, string CNPJ, string ContactPerson, string Email, string PhoneNumber,
        int? Number,
        string? Street,
        string? City,
        string? State,
        string? ZipCode, string? Notes);
}
