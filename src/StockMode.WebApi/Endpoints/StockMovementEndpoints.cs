using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockMode.Application.Features.StockMovements.Commands.AdjustStock;
using StockMode.Application.Features.StockMovements.Dtos;
using StockMode.Application.Features.StockMovements.Queries.GetFullStockMovementReport;
using StockMode.Application.Features.StockMovements.Queries.GetMovementHistoryByVariationId;
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

            group.MapGet("/movement-history/{variationId:int}", HandleGetMovementHistoryByVariationId)
                .WithName("GetMovementHistoryByVariationId")
                .Produces<IEnumerable<StockMovementDetailsDto>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Get Movement History by Variation ID")
                .WithDescription("Retrieves stock movement history for a specific product variation.");

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
            [FromBody] AdjustStockCommand command,
            [FromServices] IMediator mediator)
        {
            await mediator.Send(command);
            return Results.NoContent();
        }

        private static async Task<IResult> HandleGetFullStockMovement([FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate,
            [FromServices] IMediator mediator)
        {
            var query = new GetFullStockMovementReportQuery(startDate, endDate);
            var result = await mediator.Send(query);
            return Results.Ok(result);
        }

        private static async Task<IResult> HandleGetMovementHistoryByVariationId(
            [FromRoute] int variationId,
            [FromServices] IMediator mediator)
        {
            var query = new GetMovementHistoryByVariationIdQuery(variationId);
            var result = await mediator.Send(query);
            return result is not null ? Results.Ok(result) : Results.NotFound();
        }

        private static async Task<IResult> HandleGetStockAdjustmentRepost(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate,
            [FromServices] IMediator mediator)
        {
            var query = new GetStockAdjustmentReportQuery(startDate, endDate);
            var result = await mediator.Send(query);
            return Results.Ok(result);
        }
        #endregion
    }
}
