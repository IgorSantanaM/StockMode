using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.Tags.Commands.CreateTag
{
    public record CreateTagCommand(string? Name, string? Color) : IRequest<int>;
}
