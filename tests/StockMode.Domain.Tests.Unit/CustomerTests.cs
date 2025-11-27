using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using StockMode.Domain.Core.Exceptions;
using StockMode.Domain.Customers;
using StockMode.Domain.Tags;
using StockMode.Domain.ValueObjects;

namespace StockMode.Domain.Tests.Unit
{
    public class CustomerTests
    {
        [Theory]
        [MemberData(nameof(AddValidTestCustomers))]
        public void CreateCustomer_ShouldSucceed_WithValidParameters(Customer customer)
        {
            // Arrange & Act
            var result = new Customer(customer.Name, customer.Email, customer.PhoneNumber, customer.Address, customer.Notes);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(customer.Name);
            result.Email.Should().Be(customer.Email);
            result.PhoneNumber.Should().Be(customer.PhoneNumber);
            result.Address.Should().Be(customer.Address);
            result.Notes.Should().Be(customer.Notes);
        }

        [Theory]
        [MemberData(nameof(AddInvalidTestCustomers))]
        public void CreateCustomer_ShouldThrowDomainException_WithInvalidParameters(PlainCustomer customer, string expectedErrorMessage)
        {
            // Arrange & Act
            Action act = () => new Customer(customer.Name!, customer.Email!, customer.PhoneNumber!, customer.Address, customer.Notes);

            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage(expectedErrorMessage);
        }

        [Theory]
        [MemberData(nameof(AddValidTestCustomers))]
        public void AddTagToAnExistingCustomer_ShouldSucceed(Customer customer)
        {
            // Arrange
            Tag tag = new Tag("VIP Customer", "red");
            tag.Id = 1;

            var result = new Customer(customer.Name, customer.Email, customer.PhoneNumber, customer.Address, customer.Notes);


            // Act
            result.AddTag(tag.Id);

            // Assert
            result.TagIds.Should().Contain(tag.Id);
        }

        [Theory]
        [MemberData(nameof(AddValidTestCustomers))]
        public void RemoveTagFromAnExistingCustomer_ShouldSucceed(Customer customer)
        {
            // Arrange
            Tag tag = new Tag("Loyal Customer", "blue");
            tag.Id = 2;
            var result = new Customer(customer.Name, customer.Email, customer.PhoneNumber, customer.Address, customer.Notes);

            result.AddTag(tag.Id);

            // Act
            result.RemoveTag(tag.Id);

            // Assert
            result.TagIds.Should().NotContain(tag.Id);
        }

        [Theory]
        [MemberData(nameof(AddValidTestCustomers))]
        public void UpdateTagOnAnExistingCustomer_ShouldSucceed(Customer customer)
        {
            // Arrange
            Tag oldTag = new Tag("Occasional Buyer", "yellow");
            oldTag.Id = 5;

            Tag newTag = new Tag("Regular Buyer", "orange");
            newTag.Id = 6;

            var result = new Customer(customer.Name, customer.Email, customer.PhoneNumber, customer.Address, customer.Notes);
            result.AddTag(oldTag.Id);

            List<int> tagList = new() { oldTag.Id, newTag.Id};

            // Act
            result.UpdateTags(tagList);

            // Assert
            result.TagIds.Should().Contain(oldTag.Id);
            result.TagIds.Should().Contain(newTag.Id);
        }

        [Theory]
        [MemberData(nameof(AddValidTestCustomers))]
        public void ClearTagsFromAnExistingCustomer_ShouldSucceed(Customer customer)
        {
            // Arrange
            Tag tag1 = new Tag("Frequent Buyer", "green");
            tag1.Id = 3;
            Tag tag2 = new Tag("Newsletter Subscriber", "purple");
            tag2.Id = 4;
            var result = new Customer(customer.Name, customer.Email, customer.PhoneNumber, customer.Address, customer.Notes);

            result.AddTag(tag1.Id);
            result.AddTag(tag2.Id);

            // Act
            result.ClearTags();

            // Assert
            result.TagIds.Should().BeEmpty();
        }

        [Theory]
        [MemberData(nameof(AddValidTestCustomers))]
        public void UpdateCustomerDetails_ShouldSucceed_WithValidParameters(Customer customer)
        {
            // Arrange
            var result = new Customer(customer.Name, customer.Email, customer.PhoneNumber, customer.Address, customer.Notes);

            var newName = "Updated Name";
            var newEmail = "newemail@gmail.com";
            var newPhoneNumber = "0987654321";
            var newAddress = new Address(456, "New City", "New State", "54321", "New Country");
            var newNotes = "Updated notes";

            // Act
            result.UpdateDetails(newName, newEmail, newPhoneNumber, newAddress, newNotes);
            Action act = () => result.UpdateDetails(newName, newEmail, newPhoneNumber, newAddress, newNotes);

            // Assert

            act.Should().NotThrow<DomainException>();
            act.Should().NotBeNull();
            result.Name.Should().Be(newName);
            result.Email.Should().Be(newEmail);
            result.PhoneNumber.Should().Be(newPhoneNumber);
            result.Address.Should().Be(newAddress);
            result.Notes.Should().Be(newNotes);
        }

        [Theory]
        [MemberData(nameof(AddInvalidTestCustomers))]
        public void UpdateCustomerDetails_ShouldThrowDomainException_WithInvalidParameters(PlainCustomer customer, string expectedErrorMessage)
        {
            // Arrange
            var existingCustomer = new Customer("Igor Santana", "igor@gmail.com", "1231231234", new Address(789, "Old City", "Old State", "67890", "Old Country"), "Existing customer");
            
            // Act
            Action act = () => existingCustomer.UpdateDetails(customer.Name, customer.Email, customer.PhoneNumber, customer.Address, customer.Notes);

            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage(expectedErrorMessage);
        }

        public static IEnumerable<object[]> AddValidTestCustomers =>
            new List<object[]>
            {
               new object [] {
                   new Customer("John Doe", "test@gmail.com", "1234567890", new Address(123, "City", "State", "12345", "Country"), "Regular customer")
               },
               new object[] {
                   new Customer("Alice Johnson", "alice.johnson@example.com","5551239876",new Address(456, "Springfield", "Illinois", "62704", "USA"), "Premium member")
               },
               new object[] {
                   new Customer("Michael Smith","m.smith@example.com","9876543210", new Address(789, "Riverside", "California", "92501", "USA"), "Loyal customer")
               },
               new object[] {
                   new Customer("Emma Brown", "emma.brown@example.com", "4448882222",new Address(321, "Lakeside", "Ohio", "43004", "USA"),"New customer")
               }
            };

        public static IEnumerable<object[]> AddInvalidTestCustomers =>
             new List<object[]>
             {
                new object[] {
                    new PlainCustomer(
                        null!,
                        "valid.email@example.com",
                        "5551112222",
                        new Address(101, "Test City", "Test State", "10001", "Test Country"),
                        "Invalid name test: null"
                    ),
                    "Customer name cannot be empty."
                },

                new object[] {
                    new PlainCustomer(
                        "",
                        "another.valid@example.com",
                        "5553334444",
                        new Address(102, "Test City", "Test State", "10002", "Test Country"),
                        "Invalid name test: empty string"
                    ),
                    "Customer name cannot be empty."
                },

                new object[] {
                    new PlainCustomer(
                        "  ",
                        "email@test.com",
                        "5555556666",
                        new Address(103, "Test City", "Test State", "10003", "Test Country"),
                        "Invalid name test: whitespace"
                    ),
                    "Customer name cannot be empty."
                },

                new object[] {
                    new PlainCustomer(
                        "Valid Name",
                        null!,
                        "5557778888",
                        new Address(104, "Test City", "Test State", "10004", "Test Country"),
                        "Invalid email test: null"
                    ),
                    "Email cannot be empty."
                },

                new object[] {
                    new PlainCustomer(
                        "Another Valid Name",
                        "",
                        "5559990000",
                        new Address(105, "Test City", "Test State", "10005", "Test Country"),
                        "Invalid email test: empty string"
                    ),
                    "Email cannot be empty."
                },

                new object[] {
                    new PlainCustomer(
                        "A Third Valid Name",
                        " \t ",
                        "5550001111",
                        new Address(106, "Test City", "Test State", "10006", "Test Country"),
                        "Invalid email test: whitespace"
                    ),
                    "Email cannot be empty."
                },

                new object[] {
                    new PlainCustomer(
                        "Phone Test Customer",
                        "phone.test@example.com",
                        null!,
                        new Address(107, "Test City", "Test State", "10007", "Test Country"),
                        "Invalid phone test: null"
                    ),
                    "Phone number cannot be empty."
                },

                new object[] {
                    new PlainCustomer(
                        "Phone Test Customer 2",
                        "phone.test2@example.com",
                        "",
                        new Address(108, "Test City", "Test State", "10008", "Test Country"),
                        "Invalid phone test: empty string"
                    ),
                    "Phone number cannot be empty."
                },

                new object[] {
                    new PlainCustomer(
                        "Phone Test Customer 3",
                        "phone.test3@example.com",
                        "   ",
                        new Address(109, "Test City", "Test State", "10009", "Test Country"),
                        "Invalid phone test: whitespace"
                    ),
                    "Phone number cannot be empty."
                }
             };

        // Lightweight DTO to provide test parameters without invoking domain validation
        public class PlainCustomer
        {
            public string? Name { get; }
            public string? Email { get; }
            public string? PhoneNumber { get; }
            public Address Address { get; }
            public string? Notes { get; }

            public PlainCustomer(string? name, string? email, string? phoneNumber, Address address, string? notes)
            {
                Name = name;
                Email = email;
                PhoneNumber = phoneNumber;
                Address = address;
                Notes = notes;
            }
        }
    }
}
