using StockMode.Domain.Enums;

namespace StockMode.Application.Features.StockMovements.Dtos;

public record StockAdjustmentReportDto(
    DateTime MovementDate,
    int VariationId,
    StockMovementType MovementType,
    int Quantity,
    int StockAfterMovement,
    string? Observation
);  