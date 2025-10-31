using MediatR;
using StockMode.Application.Exceptionns;
using StockMode.Domain.Tags;

namespace StockMode.Application.Features.Tags.Commands.UpdateTag
{
    public class UpdateTagCommandHandler(ITagRepository tagRepository) : IRequestHandler<UpdateTagCommand>
    {
        public async Task Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            var tag = await tagRepository.GetTagById(request.Id);

            if (tag is null)
                throw new NotFoundException($"Tag not found with id: {request.Id}", typeof(Tag));

            tag.UpdateDetails(request.Name, request.Color);
            tagRepository.Update(tag);
        }
    }
}
