using Bogus;
using Bogus.Extensions.UnitedKingdom;
using FluentAssertions;
using StockMode.Application.Common.Dtos;
using StockMode.Application.Features.Customers.Commands.CreateCustomer;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace StockMode.Presentation.WebApi.Tests.Integration.Endpoints.CustomerEndpoints
{
    public class CreateCustomerEndpointTests : IClassFixture<StockModeApiFactory>
    {
        private readonly Faker<CreateCustomerCommand> _customerGenerator = new Faker<CreateCustomerCommand>()
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

        private readonly HttpClient _client;

        public CreateCustomerEndpointTests(StockModeApiFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateCustomer_ShouldReturnCreated_WhenRequestIsValid()
        {
            // Arrange
            var customer = _customerGenerator.Generate();

            // Act
            var response = await _client.PostAsJsonAsync("/api/customers/", customer);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }
    }
}
