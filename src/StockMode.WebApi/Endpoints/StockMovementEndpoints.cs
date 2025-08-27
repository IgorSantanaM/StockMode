using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockMode.Application.Features.StockMovements.Commands.AdjustStock;
using StockMode.Application.Features.StockMovements.Commands.ReceivePurchaseOrder;
using StockMode.Application.Features.StockMovements.Dtos;
using StockMode.Application.Features.StockMovements.Queries.GetFullStockMovementReport;
using StockMode.Application.Features.StockMovements.Queries.GetStockAdjustmentReport;
using StockMode.WebApi.Endpoints.Internal;

namespace StockMode.WebApi.Endpoints
{
    public class StockMovementEndpoints : IEndpoints
    {
        public static void DefineEndpoint(WebApplication app)
        {
            var group = app.MapGroup("/api/stock-movements")
                .WithTags("Stock Movements");

            group.MapPut("/adjust", HandleAdjustStock)
              .WithName("AdjustStock")
              .Produces(StatusCodes.Status204NoContent)
              .Produces(StatusCodes.Status400BadRequest)
              .ProducesProblem(StatusCodes.Status500InternalServerError)
              .WithSummary("Adjust the Stock.");

            group.MapPost("/receive-purchase-order", HandleReceivePurchaseOrder)
              .WithName("ReceivePurchaseOrder")
              .Produces(StatusCodes.Status204NoContent)
              .Produces(StatusCodes.Status400BadRequest)
              .ProducesProblem(StatusCodes.Status500InternalServerError)
              .WithSummary("Receive a Purchase Order and adjust stock accordingly.");

            group.MapGet("/full-movement-report", HandleGetFullStockMovement)
                .WithName("GetFullStockMovementReport")
                .Produces<IEnumerable<StockMovementDetailsDto>>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Get Full Stock Movement Report")
                .WithDescription("Retrieves a full report of stock movements between specified dates.");

            group.MapGet("/stock-adjustment-report", HandleGetStockAdjustmentRepost)
                .WithName("GetStockAdjustmentReport")
                .Produces<IEnumerable<StockMovementDetailsDto>>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Get Stock Adjustment Report")
                .WithDescription("Retrieves a report of stock adjustments made between specified dates.");
        }

        #region Handlers

        private static async Task<IResult> HandleAdjustStock(
            [FromBody] AdjustStockCommand command,
            [FromServices] IMediator mediator)
        {
            await mediator.Send(command);
            return Results.NoContent();
        }

        private static async Task<IResult> HandleReceivePurchaseOrder(
            [FromBody] ReceivePurchaseOrderCommand command,
            [FromServices] IMediator mediator)
        {
            await mediator.Send(command);
            return Results.NoContent();
        }
        private static async Task<IResult> HandleGetFullStockMovement([FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromQuery] int? variationId,
            [FromQuery] int page,
            [FromQuery] int pageSize,
            [FromServices] IMediator mediator)
        {
            var query = new GetFullStockMovementReportQuery(startDate, endDate, variationId, page, pageSize);
            var result = await mediator.Send(query);
            return Results.Ok(result);
        }

        private static async Task<IResult> HandleGetStockAdjustmentRepost(
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromQuery] int page,
            [FromQuery] int pageSize,
            [FromServices] IMediator mediator)
        {
            var query = new GetStockAdjustmentReportQuery(startDate, endDate, page, pageSize);
            var result = await mediator.Send(query);
            return Results.Ok(result);
        }
        #endregion
    }
}
