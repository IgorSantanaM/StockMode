using MediatR;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using StockMode.Application.Features.Customers.Commands.CreateCustomer;
using StockMode.Application.Features.Customers.Commands.DeleteCustomer;
using StockMode.Application.Features.Customers.Commands.UpdateCustomer;
using StockMode.Application.Features.Customers.Queries.GetAllCustomers;
using StockMode.Application.Features.Customers.Queries.GetCustomerById;
using StockMode.WebApi.Diagnostics;
using StockMode.WebApi.Endpoints.Internal;
using System.Diagnostics;

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
        }

        private static async Task<IResult> HandleCreateCustomer([FromBody] CreateCustomerCommand createCustomerCommand, [FromServices] IMediator mediator)
        {
            var id = await mediator.Send(createCustomerCommand);

            ApplicationDiagnostics.CustomerCreatedCounter.Add(1, new KeyValuePair<string, object?>("customer-id", id));
            return Results.CreatedAtRoute($"GetCustomerById", new { id  });
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

        private static async Task<IResult> HandleGetAllCustomers(string? name, int page, int pageSize, [FromServices] IMediator mediator)
        {
            var query = new GetAllCustomersQuery(name, page, pageSize); 
            var customers = await mediator.Send(query);
            return Results.Ok(customers);
        }
    }
}
