using Dapper;
using MediatR;
using StockMode.Application.Features.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDetailsDto>
    {
        private readonly IDbConnection _dbConnection;

        public GetProductByIdQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<ProductDetailsDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            const string sql = @"
                SELECT
                    p.""Id"", p.""Name"", p.""Description"", p.""IsActive"",
                    v.""Id"", v.""Name"", v.""Sku"", v.""CostPrice"", v.""SalePrice"", v.""StockQuantity""
                FROM ""Products"" AS p
                LEFT JOIN ""Variation"" AS v ON p.""Id"" = v.""ProductId""
                WHERE p.""Id"" = @ProductId;";

            var productDictionary = new Dictionary<int, ProductDetailsDto>();

            await _dbConnection.QueryAsync<ProductDetailsDto, VariationDetailDto, ProductDetailsDto>(
                sql,
                (product, variation) =>
                {
                    if (!productDictionary.TryGetValue(product.Id, out var productEntry))
                    {
                        productEntry = product;
                        productDictionary.Add(productEntry.Id, productEntry);
                    }

                    if (variation != null)
                    {
                        var variations = productEntry.Variations.ToList();
                        variations.Add(variation);
                        productEntry = productEntry with { Variations = variations };
                        productDictionary[product.Id] = productEntry;
                    }

                    return productEntry;
                },
                new { request.ProductId },
                splitOn: "Id");

            return productDictionary.Values.FirstOrDefault()!;
        }
    }
}
