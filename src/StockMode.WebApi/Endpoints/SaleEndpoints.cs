using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockMode.Application.Features.Sales.Commands.ApplyDiscountToSale;
using StockMode.Application.Features.Sales.Commands.CancelSale;
using StockMode.Application.Features.Sales.Commands.ChangeSalePaymentMethod;
using StockMode.Application.Features.Sales.Commands.CompleteSale;
using StockMode.Application.Features.Sales.Commands.CreateSale;
using StockMode.Application.Features.Sales.Commands.CreateSaleItem;
using StockMode.Application.Features.Sales.Commands.DeleteSale;
using StockMode.Application.Features.Sales.Dtos;
using StockMode.Application.Features.Sales.Queries.GetAllSales;
using StockMode.Application.Features.Sales.Queries.GetSaleById;
using StockMode.Domain.Enums;
using StockMode.WebApi.Endpoints.Internal;

namespace StockMode.WebApi.Endpoints
{
    public class SaleEndpoints : IEndpoints
    {
        public static void DefineEndpoint(WebApplication app)
        {
            var group = app.MapGroup("api/sales").WithTags("Sales");

            group.MapPost("/", HandleCreateSale)
              .WithName("CreateSale")
              .Produces<object>(StatusCodes.Status201Created)
              .ProducesProblem(StatusCodes.Status500InternalServerError)
              .WithSummary("Creates a new Sale.")
              .WithDescription("Creates a new Sale with the specified details. Returns the created product's ID on success.");

            group.MapPost("/add-item", HandleAddItemToSale)
              .WithName("AddItemToSale")
              .Produces(StatusCodes.Status204NoContent)
              .ProducesProblem(StatusCodes.Status500InternalServerError)
              .WithSummary("Adds an item to an existing Sale.")
              .WithDescription("Adds a new item to the specified Sale. Returns no content on success.");

            group.MapPut("/apply-discount", HandleApplyDiscountToSale)
              .WithName("ApplyDiscountToSale")
              .Produces(StatusCodes.Status204NoContent)
              .ProducesProblem(StatusCodes.Status404NotFound)
              .ProducesProblem(StatusCodes.Status500InternalServerError)
              .WithSummary("Applies a discount to an existing Sale.")
              .WithDescription("Applies a discount to the specified Sale. Returns no content on success.");

            group.MapPut("/complete/{id:int}/{customerId:int}", HandleCompleteSale)
                .WithName("CompleteSale")
                .Produces(StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Completes an existing Sale.")
                .WithDescription("Completes the specified Sale. Returns no content on success.");

            group.MapPut("/cancel/{id:int}", HandleCancelSale)
                .WithName("CancelSale")
                .Produces(StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Cancels an existing Sale.")
                .WithDescription("Cancels the specified Sale. Returns no content on success.");

            group.MapPut("/change-payment-method", HandleChangeSalePaymentMethod)
                .WithName("ChangeSalePaymentMethod")
                .Produces(StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Changes the payment method of an existing Sale.")
                .WithDescription("Changes the payment method of the specified Sale. Returns no content on success.");

            group.MapGet("/", HandleGetAllSales)
                .WithName("GetAllSales")
                .Produces<IEnumerable<SaleSummaryDto>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithSummary("Retrieves all sales.");

            group.MapGet("/{id:int}", HandleGetSaleById)
              .WithName("GetSaleById")
              .Produces<SaleDetailsDto>(StatusCodes.Status200OK)
              .ProducesProblem(StatusCodes.Status404NotFound)
              .ProducesProblem(StatusCodes.Status500InternalServerError)
              .WithSummary("Retrieves a Sale by its ID.")
              .WithDescription("Retrieves the details of a Sale by its ID. Returns 404 if not found.");

            group.MapDelete("{id:int}", HandleDeleteSale)
                .WithName("DeleteSale")
                .Produces(StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Deletes a Sale by its ID.")
                .WithDescription("Deletes the specified Sale by its ID. Returns no content on success.");
        }

        #region Handlers
        private static async Task<IResult> HandleCreateSale([FromBody] CreateSaleCommand command,
            [FromServices] IMediator mediator)
        {
            var id = await mediator.Send(command);
            return Results.Ok(id);
        }
        private static async Task<IResult> HandleAddItemToSale([FromBody] AddItemToSaleCommandDto command,
            [FromServices] IMediator mediator)
        {
            await mediator.Send(command);
            return Results.NoContent();
        }

        private static async Task<IResult> HandleGetAllSales(DateTime? startDate, DateTime? endDate, SaleStatus? status, int page, int pageSize, [FromServices] IMediator mediator)
        {
            var query = new GetAllSalesQuery(startDate, endDate, status, page, pageSize);
            var sales = await mediator.Send(query);
            return Results.Ok(sales);
        }

        private static async Task<IResult> HandleGetSaleById([FromRoute] int id, [FromServices] IMediator mediator)
        {
            var query = new GetSaleByIdQuery(id);
            var sale = await mediator.Send(query);
            return Results.Ok(sale);
        }

        private static async Task<IResult> HandleApplyDiscountToSale([FromBody] ApplyDiscountToSaleCommand command,
            [FromServices] IMediator mediator)
        {
            await mediator.Send(command);
            return Results.NoContent();
        }

        private static async Task<IResult> HandleCompleteSale([FromRoute] int id, [FromRoute] int customerId,
            [FromServices] IMediator mediator)
        {
            var command = new CompleteSaleCommand(id, customerId);
            await mediator.Send(command);
            return Results.NoContent();
        }

        private static async Task<IResult> HandleCancelSale([FromRoute] int id,
            [FromServices] IMediator mediator)
        {
            var command = new CancelSaleCommand(id);
            await mediator.Send(command);
            return Results.NoContent();
        }

        private static async Task<IResult> HandleChangeSalePaymentMethod([FromBody] ChangeSalePaymentMethodCommand command,
            [FromServices] IMediator mediator)
        {
            await mediator.Send(command);
            return Results.NoContent();
        }

        private static async Task<IResult> HandleDeleteSale([FromRoute] int id, [FromServices] IMediator mediator)
        {
            var command = new DeleteSaleCommand(id);
            await mediator.Send(command);
            return Results.NoContent();
        }
        #endregion
    }
}
