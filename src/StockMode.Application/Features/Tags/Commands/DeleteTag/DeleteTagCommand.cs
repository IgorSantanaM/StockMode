using MediatR;

namespace StockMode.Application.Features.Tags.Commands.DeleteTag;

public record DeleteTagCommand(int Id) : IRequest;
