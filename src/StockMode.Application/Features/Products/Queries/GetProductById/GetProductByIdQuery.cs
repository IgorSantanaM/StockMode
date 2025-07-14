using MediatR;
using StockMode.Application.Features.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.Products.Queries.GetProductById
{
    public record GetProductByIdQuery(int ProductId) : IRequest<ProductDetailsDto>;
}
