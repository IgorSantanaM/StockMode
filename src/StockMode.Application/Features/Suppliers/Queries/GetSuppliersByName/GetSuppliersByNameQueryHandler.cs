using Dapper;
using MediatR;
using StockMode.Application.Features.Suppliers.Dtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.Suppliers.Queries.GetSuppliersByName
{
    public class GetSuppliersByNameQueryHandler : IRequestHandler<GetSuppliersByNameQuery, IEnumerable<SupplierSummaryDto>>
    {
        private readonly IDbConnection _dbConnection;

        public GetSuppliersByNameQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<SupplierSummaryDto>> Handle(GetSuppliersByNameQuery request, CancellationToken cancellationToken)
        {
            const string sql = @"
                SELECT
                    s.""Id"",
                    s.""Name"",
                    s.""ContactPerson"",
                    s.""Email"",
                    s.""PhoneNumber""
                FROM ""Suppliers"" AS s
                WHERE s.""Name"" ILIKE @SearchTerm
                ORDER BY s.""Name"";";

            var searchTerm = $"%{request.Name}%";
            return await _dbConnection.QueryAsync<SupplierSummaryDto>(sql, new { SearchTerm = searchTerm });
        }
    }
}
