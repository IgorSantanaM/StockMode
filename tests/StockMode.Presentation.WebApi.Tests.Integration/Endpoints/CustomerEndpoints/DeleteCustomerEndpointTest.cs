using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using StockMode.Application.Common.Dtos;
using StockMode.Application.Features.Customers.Commands.CreateCustomer;
using StockMode.Application.Features.Customers.Commands.UpdateCustomer;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;

namespace StockMode.Presentation.WebApi.Tests.Integration.Endpoints.CustomerEndpoints
{
    public class DeleteCustomerEndpointTest : IClassFixture<StockModeApiFactory>
    {
        private const string REQUEST_URI = "/api/customers";
        private readonly HttpClient _client;
        private readonly Faker<CreateCustomerCommand> _createCustomer = new Faker<CreateCustomerCommand>()
            .CustomInstantiator(faker => new CreateCustomerCommand(
                faker.Person.FullName,
                faker.Person.Email,
                "(67) 99215-8961",
                new AddressDto
                (
                    faker.Random.Int(0, 1000),
                    faker.Address.StreetName(),
                    faker.Address.City(),
                    faker.Address.State(),
                    faker.Address.ZipCode()
                ),
                null,
                faker.Lorem.Sentence()
            ));

        public DeleteCustomerEndpointTest(StockModeApiFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task DeleteCustomer_ShouldSucceed_WhenParametersAreValid()
        {
            // Arrange
            var createCustomer = _createCustomer.Generate();
            var response = await _client.PostAsJsonAsync(REQUEST_URI, createCustomer);
            var location = response.Headers.Location?.ToString();
            var customerId = int.Parse(location?.Split('/').Last()!);

            // Act
            var deleteResponse = await _client.DeleteAsync(REQUEST_URI + $"/{customerId}");

            // Assert
            deleteResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteCustomer_ShouldReturnNotFound_WhenCustomerDontExist()
        {
            // Arrange
            int nonExistingCustomerId = 67; 

            // Act
            var deleteResponse = await _client.DeleteAsync(REQUEST_URI + $"/{nonExistingCustomerId}");

            // Assert
            deleteResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(null!)]
        public async Task DeleteCustomer_ShouldReturnBadRequest_WhenCustomerIdIsInvalid(int invalidCustomerId)
        {
            // Act
            var deleteResponse = await _client.DeleteAsync(REQUEST_URI + $"/{invalidCustomerId}");

            // Assert
            deleteResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
