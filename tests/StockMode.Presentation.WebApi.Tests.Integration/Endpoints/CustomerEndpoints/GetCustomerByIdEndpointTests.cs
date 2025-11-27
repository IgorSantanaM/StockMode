using Bogus;
using FluentAssertions;
using StockMode.Application.Common.Dtos;
using StockMode.Application.Features.Customers.Commands.CreateCustomer;
using StockMode.Application.Features.Customers.Dtos;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace StockMode.Presentation.WebApi.Tests.Integration.Endpoints.CustomerEndpoints
{
    [Collection("Get Customer By Id Collection")]
    public class GetCustomerByIdEndpointTests : IClassFixture<StockModeApiFactory>
    {
        private const string BASE_URI = "/api/customers";
        private readonly HttpClient _httpClient;
        private readonly Faker<CreateCustomerCommand> customerGenerator = new Faker<CreateCustomerCommand>()
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


        public GetCustomerByIdEndpointTests(StockModeApiFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task GetCustomerById_ShouldReturnCustomer_WhenCustomerExists()
        {
            // Arrange
            var customer = customerGenerator.Generate();
            var httpResponse = await _httpClient.PostAsJsonAsync(BASE_URI, customer);
            httpResponse.EnsureSuccessStatusCode();
            var location = httpResponse.Headers.Location?.ToString();
            var customerId = location!.Split('/').Last();

            // Act
            var response = await _httpClient.GetAsync(BASE_URI + $"/{customerId}");

            // Assert
            response.Content.Should().NotBeNull();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            location.Should().NotBeNull();
        }

        [Fact]
        public async Task GetCustomerById_ShouldReturnNotFound_WhenCustomerDoesntExist()
        {
            // Arrange 
            var customer = customerGenerator.Generate();
            var httpResponse = await _httpClient.PostAsJsonAsync(BASE_URI, customer);
            httpResponse.EnsureSuccessStatusCode();
            var location = httpResponse.Headers.Location?.ToString();
            var customerId = int.Parse(location!.Split('/').Last()) + 1;

            // Act

            var response = await _httpClient.GetAsync(BASE_URI + $"/{customerId}");

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
    }
}
