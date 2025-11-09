using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockMode.Application.Features.Tags.Commands.CreateTag;
using StockMode.Application.Features.Tags.Commands.DeleteTag;
using StockMode.Application.Features.Tags.Commands.UpdateTag;
using StockMode.Application.Features.Tags.Queries.GetAllTags;
using StockMode.Application.Features.Tags.Queries.GetTagById;
using StockMode.WebApi.Endpoints.Internal;
using System.Net.NetworkInformation;
using System.Xml.Linq;

namespace StockMode.WebApi.Endpoints
{
    public class TagEndpoints : IEndpoints
    {
        public static void DefineEndpoint(WebApplication app)
        {
            var group = app.MapGroup("api/tags").WithTags("Tags");

            group.MapPost("/", HandleCreateTag)
                .Produces(StatusCodes.Status201Created)
                .WithName("CreateTag");

            group.MapPut("/{id:int}", HandleUpdateTag)
                .Produces(StatusCodes.Status204NoContent)
                .WithName("UpdateTag");

            group.MapDelete("/{id:int}", HandleDeleteTag)
                .Produces(StatusCodes.Status204NoContent)
                .WithName("DeleteTag");

            group.MapGet("/", HandleGetAllTags)
                .Produces(StatusCodes.Status200OK)
                .WithName("GetAllTags");

            group.MapGet("/{id:int}", HandleGetTagById)
                .Produces(StatusCodes.Status200OK)
                .WithName("GetTagById");
        }

        #region Handlers

        private static async Task<IResult> HandleCreateTag([FromBody] CreateTagCommand createTagCommand, IMediator mediator)
        {
            var tagId = await mediator.Send(createTagCommand);
            return Results.CreatedAtRoute("GetTagById", new { id = tagId }, null);
        }

        private static async Task<IResult> HandleUpdateTag(int id, [FromBody] UpdateTagCommand updateTagCommand, IMediator mediator)
        {
            await mediator.Send(updateTagCommand);
            return Results.NoContent();
        }

        private static async Task<IResult> HandleDeleteTag(int id, IMediator mediator)
        {
            await mediator.Send(new DeleteTagCommand(id));
            return Results.NoContent();
        }
        private static async Task<IResult> HandleGetAllTags(IMediator mediator, string? name, int page = 1, int pagesize = 10)
        {
            var query = new GetAllTagsQuery(page, pagesize, name);
            var pagedResult = await mediator.Send(query);
            return Results.Ok(pagedResult);
        }
        private static async Task<IResult> HandleGetTagById(int id, IMediator mediator)
        {
            var query = new GetTagByIdQuery(id);
            var tag = await mediator.Send(query);
            return Results.Ok(tag);
        }

        #endregion
    }
}
