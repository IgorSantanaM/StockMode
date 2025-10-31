using MediatR;
using StockMode.Application.Exceptionns;
using StockMode.Domain.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.Tags.Commands.DeleteTag
{
    public class DeleteTagCommandHandler(ITagRepository tagRepository) : IRequestHandler<DeleteTagCommand>
    {
        public async Task Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            var tag =  await tagRepository.GetTagById(request.Id);

            if (tag is null)
                throw new NotFoundException($"Tag not found with id: {request.Id}", typeof(Tag));

             tagRepository.Delete(tag);
        }
    }
}
