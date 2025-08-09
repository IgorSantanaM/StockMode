using Dapper;
using MediatR;
using StockMode.Application.Features.Suppliers.Dtos;
using System.Data;

namespace StockMode.Application.Features.Suppliers.Queries.GetAllSuppliers
{
    public class GetAllSuppliersQueryHandler : IRequestHandler<GetAllSuppliersQuery, IEnumerable<SupplierSummaryDto>>
    {
        private readonly IDbConnection _dbConnection;

        public GetAllSuppliersQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<SupplierSummaryDto>> Handle(GetAllSuppliersQuery request, CancellationToken cancellationToken)
        {
            const string sql = @"
                SELECT
                    s.""Id"",
                    s.""Name"",
                    s.""ContactPerson"",
                    s.""Email"",
                    s.""PhoneNumber""
                FROM ""Suppliers"" AS s
                ORDER BY s.""Name"";";

            return await _dbConnection.QueryAsync<SupplierSummaryDto>(sql);
        }
    }
}
