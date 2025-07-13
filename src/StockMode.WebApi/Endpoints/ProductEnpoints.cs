using MediatR;
using StockMode.Application.Features.Products.Commands.CreateProduct;
using StockMode.WebApi.Endpoints.Internal;

namespace StockMode.WebApi.Endpoints
{
    public class ProductEnpoints : IEndpoints
    {
        public static void DefineEndpoint(WebApplication app)
        {
            app.MapPost("/api/products", async (CreateProductCommand product, IMediator mediator) =>
            {
                var id = await mediator.Send(product);

                return Results.CreatedAtRoute("GetProductById", new {id = id });
            }).WithName("CreateProduct")
              .Produces<object>(StatusCodes.Status201Created)
              .Produces<object>(StatusCodes.Status400BadRequest)
              .ProducesProblem(StatusCodes.Status500InternalServerError);

            app.MapGet("/api/products/{id:int}", (int id, IMediator mediator) =>
            {
                return Results.Ok(new { Id = id, Name = "Sample one." });
            }).WithName("GetProductById")
              .Produces<object>(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status404NotFound)
              .ProducesProblem(StatusCodes.Status500InternalServerError);
        }
    }
}
