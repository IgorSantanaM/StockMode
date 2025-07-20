using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockMode.Application.Exceptionns;
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
using StockMode.Application.Features.Sales.Queries.GetSalesByDateRange;
using StockMode.Application.Features.Sales.Queries.GetSalesByStatus;
using StockMode.Domain.Enums;
using StockMode.WebApi.Endpoints.Internal;
using System.Net.NetworkInformation;
using System.Threading.Tasks.Dataflow;

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

            group.MapPut("/complete/{id:int}", HandleCompleteSale)
                .WithName("CompleteSale")
                .Produces(StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Completes an existing Sale.")
                .WithDescription("Completes the specified Sale. Returns no content on success.");

            group.MapPut("/cancel/{id:int}", HandleCancelSale)
                .WithName("CancelSale")
                .Produces(StatusCodes.Status204NoContent)
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

            group.MapGet("/date-range", HandleGetSalesByDataRange)
              .WithName("GetSalesByDataRange")
              .Produces<IEnumerable<SaleDetailsDto>>(StatusCodes.Status200OK)
              .ProducesProblem(StatusCodes.Status404NotFound)
              .ProducesProblem(StatusCodes.Status500InternalServerError)
              .WithSummary("Retrieves Sales by their status.")
              .WithDescription("Retrieves all Sales that match the specified status. Returns 404 if not found.");

            group.MapGet("/status", HandleGetSalesByStatus)
              .WithName("GetSalesByStatus")
              .Produces<IEnumerable<SaleDetailsDto>>(StatusCodes.Status200OK)
              .ProducesProblem(StatusCodes.Status404NotFound)
              .ProducesProblem(StatusCodes.Status500InternalServerError)
              .WithSummary("Retrieves Sales by their status.")
              .WithDescription("Retrieves all Sales that match the specified status. Returns 404 if not found.");
        }

        #region Handlers
        public static async Task<IResult> HandleCreateSale([FromBody] CreateSaleCommand command,
            [FromServices] IMediator mediator)
        {
            try
            {
                var id = await mediator.Send(command);
                return Results.CreatedAtRoute("GetSaleById", new { Id = id });
            }
            catch (Exception)
            {
                return Results.Problem($"An unexpected error occurred while trying to create the Sale.", statusCode: 500);
            }
        }
        public static async Task<IResult> HandleAddItemToSale([FromBody] AddItemToSaleCommandDto command,
            [FromServices] IMediator mediator)
        {
            try
            {
                await mediator.Send(command);
                return Results.NoContent();
            }
            catch (Exception)
            {
                return Results.Problem($"An unexpected error occurred while trying to add the item to the sale.", statusCode: 500);
            }
        }

        public static async Task<IResult> HandleGetAllSales([FromServices] IMediator mediator)
        {
            try
            {
                var query = new GetAllSalesQuery();
                var sales = await mediator.Send(query);
                return Results.Ok(sales);
            }
            catch (NotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
            catch (Exception)
            {
                return Results.Problem($"An unexpected error occurred while trying to retrieve the sales.", statusCode: 500);
            }
        }

        public static async Task<IResult> HandleGetSaleById([FromRoute] int id, [FromServices] IMediator mediator)
        {
            try
            {
                var query = new GetSaleByIdQuery(id);
                var sale = await mediator.Send(query);
                return Results.Ok(sale);
            }
            catch (NotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
            catch (Exception)
            {
                return Results.Problem($"An unexpected error occurred while trying to retrieve the sale with ID {id}.", statusCode: 500);
            }
        }

        public static async Task<IResult> HandleGetSalesByDataRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromServices] IMediator mediator)
        {
            try
            {
                var query = new GetSalesByDataRangeQuery(startDate, endDate);
                var sales = await mediator.Send(query);
                return Results.Ok(sales);
            }
            catch (NotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
            catch (Exception)
            {
                return Results.Problem($"An unexpected error occurred while trying to retrieve the sales in the specified date range.", statusCode: 500);
            }
        }

        public static async Task<IResult> HandleGetSalesByStatus([FromQuery] SaleStatus status, [FromServices] IMediator mediator)
        {
            try
            {
                var query = new GetSalesByStatusQuery(status);
                var sales = await mediator.Send(query);
                return Results.Ok(sales);
            }
            catch (NotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
            catch (Exception)
            {
                return Results.Problem($"An unexpected error occurred while trying to retrieve the sales with status {status}.", statusCode: 500);
            }
        }

        public static async Task<IResult> HandleApplyDiscountToSale([FromBody] ApplyDiscountToSaleCommand command,
            [FromServices] IMediator mediator)
        {
            try
            {
                await mediator.Send(command);
                return Results.NoContent();
            }
            catch (NotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
            catch (Exception)
            {
                return Results.Problem($"An unexpected error occurred while trying to apply the discount to the sale.", statusCode: 500);
            }
        }

        public static async Task<IResult> HandleCompleteSale([FromRoute] int id,
            [FromServices] IMediator mediator)
        {
            try
            {
                var command = new CompleteSaleCommand(id);
                await mediator.Send(command);
                return Results.NoContent();
            }
            catch (NotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
            catch (Exception)
            {
                return Results.Problem($"An unexpected error occurred while trying to complete the sale.", statusCode: 500);
            }
        }

        public static async Task<IResult> HandleCancelSale([FromRoute] int id,
            [FromServices] IMediator mediator)
        {
            try
            {
                var command = new CancelSaleCommand(id);
                await mediator.Send(command);
                return Results.NoContent();
            }
            catch (NotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
            catch (Exception)
            {
                return Results.Problem($"An unexpected error occurred while trying to cancel the sale.", statusCode: 500);
            }
        }

        public static async Task<IResult> HandleChangeSalePaymentMethod([FromBody] ChangeSalePaymentMethodCommand command,
            [FromServices] IMediator mediator)
        {
            try
            {
                await mediator.Send(command);
                return Results.NoContent();
            }
            catch (NotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
            catch (Exception)
            {
                return Results.Problem($"An unexpected error occurred while trying to change the sale payment method.", statusCode: 500);
            }
        }

        public async Task<IResult> HandleDeleteSale([FromRoute] int id, [FromServices] IMediator mediator)
        {
            try
            {
                var command = new DeleteSaleCommand(id);
                await mediator.Send(command);
                return Results.NoContent();
            }
            catch (NotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
            catch (Exception)
            {
                return Results.Problem($"An unexpected error occurred while trying to delete the sale with ID {id}.", statusCode: 500);
            }
        }
        #endregion
    }
}
