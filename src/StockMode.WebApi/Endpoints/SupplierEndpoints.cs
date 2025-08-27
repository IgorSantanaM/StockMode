using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockMode.Application.Features.Suppliers.Commands.CreateSupplier;
using StockMode.Application.Features.Suppliers.Commands.DeleteSupplier;
using StockMode.Application.Features.Suppliers.Commands.UpdateSupplier;
using StockMode.Application.Features.Suppliers.Queries.GetAllSuppliers;
using StockMode.Application.Features.Suppliers.Queries.GetSupplierById;
using StockMode.WebApi.Endpoints.Internal;

namespace StockMode.WebApi.Endpoints
{
    public class SupplierEndpoints : IEndpoints
    {
        public static void DefineEndpoint(WebApplication app)
        {
            var group = app.MapGroup("/api/suppliers")
                .WithTags("Suppliers");

            group.MapPost("/", HandleCreateSupplier)
                .WithName("CreateSupplier")
                .Produces<int>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Creates a new supplier.")
                .WithDescription("Creates a new supplier with the specified details. Returns the created supplier's ID on success.");

            group.MapGet("/{id:int}", HandleGetSupplierById)
                .WithName("GetSupplierById")
                .Produces<object>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Retrieves a supplier by their ID.")
                .WithDescription("Gets the details of a supplier by their ID.");

            group.MapDelete("/{id:int}", HandleDeleteSupplier)
                .WithName("DeleteSupplier")
                .Produces(StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Deletes a supplier by their ID.")
                .WithDescription("Deletes the specified supplier. Returns no content on success.");

            group.MapPut("/", HandleUpdateSupplier)
                .WithName("UpdateSupplier")
                .Produces(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Updates an existing supplier.")
                .WithDescription("Updates the details of an existing supplier. Returns OK on success.");

            group.MapGet("/", HandleGetAllSuppliers)
                .WithName("GetAllSuppliers")
                .Produces<IEnumerable<object>>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Retrieves all suppliers.")
                .WithDescription("Gets a list of all suppliers in the system.");
        }

        private static async Task<IResult> HandleCreateSupplier(
            [FromBody] CreateSupplierCommand command,
            [FromServices] IMediator mediator)
        {
            var id = await mediator.Send(command);
            return Results.CreatedAtRoute("GetSupplierById", new { id });
        }

        private static async Task<IResult> HandleGetSupplierById(
            [FromRoute] int id,
            [FromServices] IMediator mediator)
        {
            var query = new GetSupplierByIdQuery(id);
            var supplier = await mediator.Send(query);
            return Results.Ok(supplier);
        }

        private static async Task<IResult> HandleDeleteSupplier(
            [FromRoute] int id,
            [FromServices] IMediator mediator)
        {
            var command = new DeleteSupplierCommand(id);
            await mediator.Send(command);
            return Results.NoContent();
        }

        private static async Task<IResult> HandleUpdateSupplier(
            [FromBody] UpdateSupplierCommand command,
            [FromServices] IMediator mediator)
        {
            await mediator.Send(command);
            return Results.Ok();
        }

        private static async Task<IResult> HandleGetAllSuppliers(string? name, int page, int pageSize,
            [FromServices] IMediator mediator)
        {
            var query = new GetAllSuppliersQuery(name, page, pageSize);
            var suppliers = await mediator.Send(query);
            return Results.Ok(suppliers);
        }
    }
}
