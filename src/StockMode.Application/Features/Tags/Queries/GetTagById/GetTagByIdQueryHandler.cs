using MediatR;
using StockMode.Application.Exceptionns;
using StockMode.Application.Features.Tags.Dtos;
using StockMode.Domain.Tags;

namespace StockMode.Application.Features.Tags.Queries.GetTagById
{
    public class GetTagByIdQueryHandler(ITagRepository tagRepository) : IRequestHandler<GetTagByIdQuery, TagDetailsDto>
    {
        public async Task<TagDetailsDto> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
        {
            var tag = await tagRepository.GetTagById(request.id);

            if (tag is null)
                throw new NotFoundException($"Tag not found with id: {request.id}", typeof(Tag));

            var tagDto = new TagDetailsDto(tag.Id, tag.Name, tag.Color);

            return tagDto;
        }
    }
}
