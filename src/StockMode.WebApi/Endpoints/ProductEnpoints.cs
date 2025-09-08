using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockMode.Application.Features.Products.Commands.CreateProduct;
using StockMode.Application.Features.Products.Commands.DeleteProduct;
using StockMode.Application.Features.Products.Commands.UpdateProduct;
using StockMode.Application.Features.Products.Dtos;
using StockMode.Application.Features.Products.Queries.GetAllProducts;
using StockMode.Application.Features.Products.Queries.GetAllVariations;
using StockMode.Application.Features.Products.Queries.GetProductById;
using StockMode.Application.Features.Products.Queries.GetVariationById;
using StockMode.WebApi.Endpoints.Internal;

namespace StockMode.WebApi.Endpoints
{
    public class ProductEnpoints : IEndpoints
    {
        public static void DefineEndpoint(WebApplication app)
        {
            var group = app.MapGroup("/api/products").WithTags("Products").RequireAuthorization();

            group.MapPost("/", HandleCreateProduct)
              .WithName("CreateProduct")
              .Produces<object>(StatusCodes.Status201Created)
              .Produces<object>(StatusCodes.Status400BadRequest)
              .ProducesProblem(StatusCodes.Status500InternalServerError)
              .WithSummary("Creates a new product.")
              .WithDescription("Creates a new product with the specified details, including its variations. Returns the created product's ID on success.");

            group.MapGet("/{id:int}", HandleGetProductById)
              .WithName("GetProductById")
              .Produces<ProductDetailsDto>(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status404NotFound)
              .ProducesProblem(StatusCodes.Status500InternalServerError)
              .WithSummary("Retrieves a product by its ID.")
              .WithDescription("Gets the details of a product by its ID, including its variations and stock information.");

            group.MapGet("/", HandleGetAllProducts)
              .WithName("GetAllProducts")
              .Produces<IEnumerable<ProductSummaryDto>>(StatusCodes.Status200OK)
              .ProducesProblem(StatusCodes.Status500InternalServerError)
              .WithSummary("Retrieves a list of all products")
              .WithDescription("Gets a summary list of all products in the system, including their price range and total stock quantity.");

            group.MapGet("/variation/{variationId:int}", HandleGetVariationById)
                .WithName("GetVariationById")
                .Produces<VariationDetailDto>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Retrieves a specific product variation by its ID.")
                .WithDescription("Gets details of a product variation, including its price and stock quantity, by providing the variation ID.");

            group.MapDelete("/{id:int}", HandleDeleteProductById)
                .WithName("DeleteProductById")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Deletes a product by its ID.")
                .WithDescription("Deletes a product from the system by providing its ID. If the product has variations, they will also be deleted.");

            group.MapPut("/{id:int}", HandleUpdateProduct)
                .WithName("UpdateProduct")
                .Produces<ProductDetailsDto>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Updates an existing product by its ID.")
                .WithDescription("Updates the details of an existing product, including its variations, by providing the product ID and updated information.");

            group.MapGet("/variations", HandleGetAllVariations)
                .WithName("GetAllVariations")
                .Produces<IEnumerable<VariationDetailDto>>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Retrieves a list of all product variations.")
                .WithDescription("Gets a summary list of all product variations in the system, including their price and stock quantity.");
        }

        #region Handlers

        private static async Task<IResult> HandleCreateProduct(
            [FromBody] CreateProductCommand product,
            IMediator mediator)
        {
            var id = await mediator.Send(product);

            return Results.CreatedAtRoute("GetProductById", new { id = id });
        }

        private static async Task<IResult> HandleGetProductById(
                [FromRoute] int id,
                IMediator mediator)
        {
            var query = new GetProductByIdQuery(id);

            var product = await mediator.Send(query);

            return product is not null ? Results.Ok(product) : Results.NotFound();
        }

        private static async Task<IResult> HandleGetAllProducts(int? lowStockThreshold,
            string? name,
            int page,
            int pageSize,
            IMediator mediator)
        {
            var query = new GetAllProductsQuery(lowStockThreshold, name, page, pageSize);
            var products = await mediator.Send(query);
            return Results.Ok(products);
        }

        private static async Task<IResult> HandleGetVariationById(
                [FromRoute] int variationId,
                IMediator mediator)
        {
            var query = new GetVariationByIdQuery(variationId);
            var variation = await mediator.Send(query);
            return variation is not null ? Results.Ok(variation) : Results.NotFound();
        }

        private static async Task<IResult> HandleDeleteProductById(
                [FromRoute] int id,
                IMediator mediator)
        {

            var command = new DeleteProductCommand(id);
            await mediator.Send(command);
            return Results.NoContent();
        }

        private static async Task<IResult> HandleUpdateProduct(
                [FromRoute] int id,
                [FromBody] UpdateProductCommand command,
                IMediator mediator)
        {
            var updateProductCommand = new UpdateProductCommand(id, command.Name, command.Description, command.Variations);
            var updatedProduct = await mediator.Send(updateProductCommand);
            return Results.Ok(updatedProduct);

        }

        private static async Task<IResult> HandleGetAllVariations(string? name, string? sku, int page, int pageSize, IMediator mediator)
        {
            var query = new GetAllVariationsQuery(name, sku, page, pageSize);
            var variations = await mediator.Send(query);

            return Results.Ok(variations);
        }
        #endregion
    }
}
