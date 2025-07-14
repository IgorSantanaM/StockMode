using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockMode.Application.Features.Products.Commands.CreateProduct;
using StockMode.Application.Features.Products.Dtos;
using StockMode.Application.Features.Products.Queries.GetAllProducts;
using StockMode.Application.Features.Products.Queries.GetProductById;
using StockMode.WebApi.Endpoints.Internal;

namespace StockMode.WebApi.Endpoints
{
    public class ProductEnpoints : IEndpoints
    {
        public static void DefineEndpoint(WebApplication app)
        {
            var group = app.MapGroup("/api/products").WithTags("Products");

            group.MapPost("/", async (CreateProductCommand product, IMediator mediator) =>
            {
                var id = await mediator.Send(product);

                return Results.CreatedAtRoute("GetProductById", new { id = id });
            }).WithName("CreateProduct")
              .Produces<object>(StatusCodes.Status201Created)
              .Produces<object>(StatusCodes.Status400BadRequest)
              .ProducesProblem(StatusCodes.Status500InternalServerError);

            group.MapGet("/{id:int}", async ([FromRoute] int id, IMediator mediator) =>
            {
                try
                {
                    var query = new GetProductByIdQuery(id);

                    var product = await mediator.Send(query);

                    return product is not null ? Results.Ok(product) : Results.NotFound();
                }
                catch (Exception)
                {
                    return Results.Problem($"An unexpected error occurred while fetching the product with ID {id}.", statusCode: 500);
                }
            }).WithName("GetProductById")
              .Produces<object>(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status404NotFound)
              .ProducesProblem(StatusCodes.Status500InternalServerError);

            group.MapGet("/", async (IMediator mediator) =>
            {
                try
                {
                    var query = new GetAllProductsQuery();
                    var products = await mediator.Send(query);
                    return Results.Ok(products);
                }
                catch (Exception ex)
                {
                    return Results.Problem("An unexpected error occurred while fetching products.", statusCode: 500);
                }
            })
            .WithName("GetAllProducts")
            .Produces<IEnumerable<ProductSummaryDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Retrieves a list of all products")
            .WithDescription("Gets a summary list of all products in the system, including their price range and total stock quantity.");
        }
    }
}
