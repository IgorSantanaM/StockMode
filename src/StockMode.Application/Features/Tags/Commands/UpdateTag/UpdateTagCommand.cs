using MediatR;

namespace StockMode.Application.Features.Tags.Commands.UpdateTag;

public record UpdateTagCommand(int Id, string Name, string? Color) : IRequest;
