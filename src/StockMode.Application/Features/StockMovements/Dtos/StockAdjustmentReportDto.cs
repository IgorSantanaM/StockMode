namespace StockMode.Application.Features.StockMovements.Dtos;

public record StockAdjustmentReportDto(
    DateTime MovementDate,
    int VariationId,
    string MovementType,
    int Quantity,
    int StockAfterMovement,
    string? Observation
);