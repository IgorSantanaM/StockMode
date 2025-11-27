using Bogus;
using Bogus.Extensions.UnitedKingdom;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using StockMode.Application.Common.Dtos;
using StockMode.Application.Features.Customers.Commands.CreateCustomer;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace StockMode.Presentation.WebApi.Tests.Integration.Endpoints.CustomerEndpoints
{
    [Collection("Create Customers Collection")]
    public class CreateCustomerEndpointTests : IClassFixture<StockModeApiFactory>
    {
        private const string REQUEST_URI = "/api/customers/";
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
            var response = await _client.PostAsJsonAsync(REQUEST_URI, customer);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }

        [Theory]
        [InlineData("invalid-email")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null!)]
        public async Task CreateCustomer_ShouldReturnBadRequest_WhenEmailIsInvalid(string? invalidEmail)
        {
            // Arrange
            var customer = _customerGenerator.Clone()
                .RuleFor(c => c.Email, faker => invalidEmail)
                .Generate();

            // Act
            var response = await _client.PostAsJsonAsync(REQUEST_URI, customer);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateCustomer_ShouldReturnBadRequest_WhenAddressIsNull()
        {
            // Arrange
            var customer = _customerGenerator.Clone()
                .RuleFor(c => c.AddressDto, faker => null!);

            // Act
            var response = await _client.PostAsJsonAsync(REQUEST_URI, customer);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(null!)]
        public async Task CreateCustoer_ShouldReturnBadRequest_WhenFullNameIsNullOrEmpty(string? fullName)
        {
            // Arrange
            var customer = _customerGenerator.Clone()
                .RuleFor(c => c.Name, faker => fullName);

            // Act
            var response = await _client.PostAsJsonAsync(REQUEST_URI, customer);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("6666-6666")]
        [InlineData("123-432")]
        [InlineData("1321 23123")]
        [InlineData("5421")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public async Task CreateCustomer_ShouldReturnBadRequest_WhenPhoneNumberIsInvalid(string? phoneNumber)
        {
            // Arrange
            var customer = _customerGenerator.Clone()
                .RuleFor(c => c.PhoneNumber, faker => phoneNumber);

            // Act
            var response = await _client.PostAsJsonAsync(REQUEST_URI, customer);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }


    }
}
