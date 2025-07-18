using MediatR;
using System.Data;
using Dapper;
using StockMode.Application.Features.Products.Dtos;

namespace StockMode.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductSummaryDto>>
    {
        private readonly IDbConnection _dbConnection;

        public GetAllProductsQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<ProductSummaryDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
                const string sql = @"
                SELECT
                    p.""Id"",
                    p.""Name"",
                    p.""Description"",
                    p.""IsActive"",
                    COUNT(v.""Id"") AS VariationCount,
                    MIN(v.""SalePrice"") AS MinSalePrice,
                    MAX(v.""SalePrice"") AS MaxSalePrice,
                    AVG(v.""CostPrice"") AS AverageCostPrice,
                    COALESCE(SUM(v.""StockQuantity""), 0) AS TotalStockQuantity
                FROM ""Products"" AS p
                LEFT JOIN ""Variation"" AS v ON p.""Id"" = v.""ProductId""
                GROUP BY p.""Id"", p.""Name"", p.""Description"", p.""IsActive""
                ORDER BY p.""Name"";";

                var products = await _dbConnection.QueryAsync<ProductSummaryDto>(sql);

                return products;
        }
    }
}
