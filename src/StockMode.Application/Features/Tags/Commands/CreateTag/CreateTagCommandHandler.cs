using FluentValidation;
using MediatR;
using StockMode.Domain.Tags;

namespace StockMode.Application.Features.Tags.Commands.CreateTag
{
    public class CreateTagCommandHandler(ITagRepository tagRepository, IValidator<CreateTagCommand> validator) : IRequestHandler<CreateTagCommand, int>
    {
        public async Task<int> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {

            var validation = validator.ValidateAndThrowAsync(request, cancellationToken);

            var tag = new Tag(request.Name, request.Color);
            await tagRepository.AddAsync(tag);

            return tag.Id;
        }
    }
}
