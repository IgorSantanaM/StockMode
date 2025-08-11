using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockMode.Application.Features.Customers.Commands.CreateCustomer;
using StockMode.Application.Features.Customers.Commands.DeleteCustomer;
using StockMode.Application.Features.Customers.Commands.UpdateCustomer;
using StockMode.Application.Features.Customers.Queries.GetAllCustomers;
using StockMode.Application.Features.Customers.Queries.GetCustomerById;
using StockMode.Application.Features.Customers.Queries.GetCustomerByName;
using StockMode.WebApi.Endpoints.Internal;

namespace StockMode.WebApi.Endpoints
{
    public class CustomerEndpoints : IEndpoints
    {
        public static void DefineEndpoint(WebApplication app)
        {
            var group = app.MapGroup("/api/customers").WithTags("Customers");

            group.MapPost("/", HandleCreateCustomer)
              .WithName("CreateCustomer")
              .Produces<object>(StatusCodes.Status201Created)
              .Produces<object>(StatusCodes.Status400BadRequest)
              .ProducesProblem(StatusCodes.Status500InternalServerError)
              .WithSummary("Creates a new customer.")
              .WithDescription("Creates a new customer with the specified details. Returns the created customer's ID on success.");

            group.MapGet("/{id:int}", HandleGetCustomerById)
                .WithName("GetCustomerById")
                .Produces<object>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Retrieves a customer by their ID.")
                .WithDescription("Gets the details of a customer by their ID.");

            group.MapDelete("/{id:int}", HandleDeleteCustomer)
                .WithName("DeleteCustomer")
                .Produces(StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Deletes a customer by their ID.")
                .WithDescription("Deletes the specified customer. Returns no content on success.");

            group.MapPut("/", HandleUpdateCustomer)
                .WithName("UpdateCustomer")
                .Produces(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Updates an existing customer.")
                .WithDescription("Updates the details of an existing customer. Returns OK on success.");

            group.MapGet("/", HandleGetAllCustomers)
                .WithName("GetAllCustomers")
                .Produces<IEnumerable<object>>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Retrieves all customers.")
                .WithDescription("Gets a list of all customers in the system.");

            group.MapGet("/search", HandleGetCustomersByName)
                .WithName("GetCustomersByName")
                .Produces<IEnumerable<object>>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Searches for customers by name.")
                .WithDescription("Gets a list of customers whose names match the specified query.");
        }

        private static async Task<IResult> HandleCreateCustomer([FromBody] CreateCustomerCommand createCustomerCommand, [FromServices] IMediator mediator)
        {
            var id = await mediator.Send(createCustomerCommand);
            return Results.CreatedAtRoute($"GetCustomerById", new { id = id  });
        }

        private static async Task<IResult> HandleGetCustomerById([FromRoute] int id, [FromServices] IMediator mediator)
        {
            var query = new GetCustomerByIdQuery(id);
            var customer = await mediator.Send(query);
            return customer is not null ? Results.Ok(customer) : Results.NotFound();
        }

        private static async Task<IResult> HandleDeleteCustomer([FromRoute] int id, [FromServices] IMediator mediator)
        {
            var command = new DeleteCustomerCommand(id);
            await mediator.Send(command);
            return Results.NoContent();
        }

        private static async Task<IResult> HandleUpdateCustomer([FromBody] UpdateCustomerCommand updateCustomerCommand, [FromServices] IMediator mediator)
        {
            await mediator.Send(updateCustomerCommand);
            return Results.Ok();
        }

        private static Task<IResult> HandleGetAllCustomers([FromServices] IMediator mediator)
        {
            var query = new GetAllCustomersQuery();
            var customers = mediator.Send(query);
            return Task.FromResult(Results.Ok(customers));
        }

        private static Task<IResult> HandleGetCustomersByName([FromQuery] string name, [FromServices] IMediator mediator)
        {
            var query = new GetCustomerByNameQuery(name);
            var customers = mediator.Send(query);
            return Task.FromResult(Results.Ok(customers));
        }

    }
}
