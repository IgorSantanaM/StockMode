using Microsoft.AspNetCore.Authorization;
using StockMode.WebApi.Endpoints.Internal;
using System.Security.Claims;

namespace StockMode.WebApi.Endpoints
{
    public class UserEndpoints : IEndpoints
    {
        public static void DefineEndpoint(WebApplication app)
        {
            var group = app.MapGroup("/api/user").WithTags("User");

            group.MapGet("/info", HandleGetUserInfo)
                .RequireAuthorization()
                .WithName("GetUserInfo")
                .Produces<object>(StatusCodes.Status200OK)
                .WithSummary("Gets authenticated user information")
                .WithDescription("Returns information about the currently authenticated user from the JWT token.");

            group.MapGet("/test", HandleTestEndpoint)
                .WithName("TestEndpoint")
                .Produces<object>(StatusCodes.Status200OK)
                .WithSummary("Test endpoint without authentication")
                .WithDescription("Simple test endpoint that doesn't require authentication.");
        }

        private static IResult HandleGetUserInfo(ClaimsPrincipal user)
        {
            var userInfo = new
            {
                Id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                Name = user.FindFirst(ClaimTypes.Name)?.Value,
                Email = user.FindFirst(ClaimTypes.Email)?.Value,
                GivenName = user.FindFirst(ClaimTypes.GivenName)?.Value,
                FamilyName = user.FindFirst(ClaimTypes.Surname)?.Value,
                Claims = user.Claims.Select(c => new { c.Type, c.Value }).ToArray()
            };

            return Results.Ok(userInfo);
        }

        private static IResult HandleTestEndpoint()
        {
            return Results.Ok(new { Message = "API is working!", Timestamp = DateTime.UtcNow });
        }
    }
}