using Dapper;
using MediatR;
using StockMode.Application.Exceptionns;
using StockMode.Application.Features.Suppliers.Dtos;
using StockMode.Domain.Suppliers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.Suppliers.Queries.GetSupplierById
{
    public class GetSupplierByIdQueryHandler : IRequestHandler<GetSupplierByIdQuery, SupplierDetailsDto>
    {
        private readonly IDbConnection _dbConnection;
        public GetSupplierByIdQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<SupplierDetailsDto> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
        {
            const string sql = @"SELECT * FROM ""Suppliers"" WHERE ""Id"" = @SupplierId;";

            var supplier = await _dbConnection.QueryFirstOrDefaultAsync<SupplierDetailsDto>(sql, new { SupplierId = request.Id });

            if (supplier == null)
                throw new NotFoundException(nameof(Supplier), request.Id);

            return supplier;
        }
    }
}
