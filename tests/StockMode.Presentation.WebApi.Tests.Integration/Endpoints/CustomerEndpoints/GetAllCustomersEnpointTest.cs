using Bogus;
using FluentAssertions;
using StockMode.Application.Common.Dtos;
using StockMode.Application.Features.Customers.Commands.CreateCustomer;
using StockMode.Application.Features.Customers.Dtos;
using StockMode.Domain.Common;
using System.Net;
using System.Net.Http.Json;

namespace StockMode.Presentation.WebApi.Tests.Integration.Endpoints.CustomerEndpoints
{
    public class GetAllCustomersEnpointTest : IClassFixture<StockModeApiFactory>
    {
        private readonly HttpClient _client;
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
           

        public GetAllCustomersEnpointTest(StockModeApiFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllCustomers_ShouldReturnAllCustomers_WhenAllParametersAreValid()
        {
            // Arrange
            var customer = _customerGenerator.Generate();
            var createResponse = await _client.PostAsJsonAsync("/api/customers", customer);
            
            // Act
            var response = await _client.GetAsync("/api/customers?page=1&pageSize=10");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Should().NotBeNull();
            var customers = await response.Content.ReadFromJsonAsync<PagedResult<CustomerSummaryDto>>();
            customers!.Items.Should().NotBeNull();
        }
    }
}
