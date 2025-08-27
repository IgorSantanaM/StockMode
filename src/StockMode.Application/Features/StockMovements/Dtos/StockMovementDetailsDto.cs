using StockMode.Domain.Enums;
using StockMode.Domain.Products;

namespace StockMode.Application.Features.StockMovements.Dtos;

public record StockMovementDetailsDto(
    int Id,
    int VariationId,
    int? SaleId,
    StockMovementType Type,
    int Quantity,
    int StockAfterMovement,
    string? Note,
    string? Observation,
    DateTime MovementDate,
    int? CustomerId,
    int? SupplierId);
