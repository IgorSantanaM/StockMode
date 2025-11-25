using FluentAssertions;
using StockMode.Domain.Core.Exceptions;
using StockMode.Domain.Customers;
using StockMode.Domain.Suppliers;
using StockMode.Domain.Tags;
using StockMode.Domain.ValueObjects;

namespace StockMode.Domain.Tests.Unit
{
    public class SupplierTests
    {

        [Theory]
        [MemberData(nameof(ValidSupplierData))]
        public void CreateSupplier_ShouldCreateSupplier_WhenParametersAreValid(PlainSupplier supplier)
        {
            // Act
            var createdSupplier = new Supplier(
                name: supplier.Name,
                corporateName: supplier.CorporateName,
                cnpj: supplier.CNPJ,
                contactPerson: supplier.ContactPerson,
                email: supplier.Email,
                phoneNumber: supplier.PhoneNumber,
                address: supplier.Address,
                notes: supplier.Notes
            );
            Action act = () => new Supplier(
                name: supplier.Name,
                corporateName: supplier.CorporateName,
                cnpj: supplier.CNPJ,
                contactPerson: supplier.ContactPerson,
                email: supplier.Email,
                phoneNumber: supplier.PhoneNumber,
                address: supplier.Address,
                notes: supplier.Notes
            );
            // Assert
            act.Should().NotThrow<Exception>();
            createdSupplier.Should().NotBeNull();
            createdSupplier.Name.Should().Be(supplier.Name);
            createdSupplier.CorporateName.Should().Be(supplier.CorporateName);
            createdSupplier.CNPJ.Should().Be(supplier.CNPJ);
            createdSupplier.ContactPerson.Should().Be(supplier.ContactPerson);
            createdSupplier.Email.Should().Be(supplier.Email);
            createdSupplier.PhoneNumber.Should().Be(supplier.PhoneNumber);
            createdSupplier.Address.Should().Be(supplier.Address);
            createdSupplier.Notes.Should().Be(supplier.Notes);
        }

        [Theory]
        [MemberData(nameof(AddInvalidTestSuppliers))]
        public void CreateSupplier_ShouldThrowDomainException_WhenParametersAreInvalid(PlainSupplier supplier, string expectedMessage)
        {
            // Act
            Action act = () => new Supplier(
                name: supplier.Name,
                corporateName: supplier.CorporateName,
                cnpj: supplier.CNPJ,
                contactPerson: supplier.ContactPerson,
                email: supplier.Email,
                phoneNumber: supplier.PhoneNumber,
                address: supplier.Address,
                notes: supplier.Notes
            );
            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage(expectedMessage);
        }

        [Theory]
        [MemberData(nameof(ValidSupplierData))]
        public void UpdateSupplier_ShouldUpdateSupplierDetails_WhenParametersAreValid(PlainSupplier supplier)
        {
            // Arrange
            var existingSupplier = new Supplier(
                name: "Old Name",
                corporateName: "Old Corporate",
                cnpj: "00.000.000/0000-00",
                contactPerson: "Old Contact",
                email: "oldemail@gmail.com",
                phoneNumber: "0000-0000",
                new Address(1, "Old St", "Old City", "OS", "00000-000"),
                notes: "Old notes");

            // Act
            Action act = () => existingSupplier.UpdateDetails(
                name: supplier.Name,
                corporateName: supplier.CorporateName,
                cnpj: supplier.CNPJ,
                contactPerson: supplier.ContactPerson,
                email: supplier.Email,
                phoneNumber: supplier.PhoneNumber,
                address: supplier.Address,
                notes: supplier.Notes
            );

            // Assert
            act.Should().NotThrow<Exception>();
            existingSupplier.Name.Should().Be(supplier.Name);
            existingSupplier.CorporateName.Should().Be(supplier.CorporateName);
            existingSupplier.CNPJ.Should().Be(supplier.CNPJ);
            existingSupplier.ContactPerson.Should().Be(supplier.ContactPerson);
            existingSupplier.Email.Should().Be(supplier.Email);
            existingSupplier.PhoneNumber.Should().Be(supplier.PhoneNumber);
            existingSupplier.Address.Should().Be(supplier.Address);
            existingSupplier.Notes.Should().Be(supplier.Notes);
        }

        [Theory]
        [MemberData(nameof(AddInvalidTestSuppliers))]
        public void UpdateSupplier_ShouldThrowDomainException_WhenParametersAreInvalid(PlainSupplier supplier, string expectedMessage)
        {
            // Arrange
            var existingSupplier = new Supplier(
                 name: "Old Name",
                 corporateName: "Old Corporate",
                 cnpj: "00.000.000/0000-00",
                 contactPerson: "Old Contact",
                 email: "oldemail@gmail.com",
                 phoneNumber: "0000-0000",
                 new Address(1, "Old St", "Old City", "OS", "00000-000"),
                 notes: "Old notes");

            // Act
            Action act = () => existingSupplier.UpdateDetails(
                name: supplier.Name,
                corporateName: supplier.CorporateName,
                cnpj: supplier.CNPJ,
                contactPerson: supplier.ContactPerson,
                email: supplier.Email,
                phoneNumber: supplier.PhoneNumber,
                address: supplier.Address,
                notes: supplier.Notes
            );

            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage(expectedMessage);
        }

        [Theory]
        [MemberData(nameof(ValidSupplierData))]
        public void AddTagToAnExistingCustomer_ShouldSucceed(PlainSupplier supplier)
        {
            // Arrange
            Tag tag = new Tag("VIP Customer", "red");
            tag.Id = 1;

            var createdSupplier = new Supplier(
                name: supplier.Name,
                corporateName: supplier.CorporateName,
                cnpj: supplier.CNPJ,
                contactPerson: supplier.ContactPerson,
                email: supplier.Email,
                phoneNumber: supplier.PhoneNumber,
                address: supplier.Address,
                notes: supplier.Notes);


            // Act
            createdSupplier.AddTag(tag.Id);

            // Assert
            createdSupplier.TagIds.Should().Contain(tag.Id);
        }

        [Theory]
        [MemberData(nameof(ValidSupplierData))]
        public void RemoveTagFromAnExistingCustomer_ShouldSucceed(PlainSupplier supplier)
        {
            // Arrange
            Tag tag = new Tag("Loyal Customer", "blue");
            tag.Id = 2;
            var createdSupplier = new Supplier(
               name: supplier.Name,
               corporateName: supplier.CorporateName,
               cnpj: supplier.CNPJ,
               contactPerson: supplier.ContactPerson,
               email: supplier.Email,
               phoneNumber: supplier.PhoneNumber,
               address: supplier.Address,
               notes: supplier.Notes);

            createdSupplier.AddTag(tag.Id);

            // Act
            createdSupplier.RemoveTag(tag.Id);

            // Assert
            createdSupplier.TagIds.Should().NotContain(tag.Id);
        }

        [Theory]
        [MemberData(nameof(ValidSupplierData))]
        public void UpdateTagOnAnExistingCustomer_ShouldSucceed(PlainSupplier supplier)
        {
            // Arrange
            Tag oldTag = new Tag("Occasional Buyer", "yellow");
            oldTag.Id = 5;

            Tag newTag = new Tag("Regular Buyer", "orange");
            newTag.Id = 6;

            var createdSupplier = new Supplier(
               name: supplier.Name,
               corporateName: supplier.CorporateName,
               cnpj: supplier.CNPJ,
               contactPerson: supplier.ContactPerson,
               email: supplier.Email,
               phoneNumber: supplier.PhoneNumber,
               address: supplier.Address,
               notes: supplier.Notes);

            createdSupplier.AddTag(oldTag.Id);

            List<int> tagList = new() { oldTag.Id, newTag.Id };

            // Act
            createdSupplier.UpdateTags(tagList);

            // Assert
            createdSupplier.TagIds.Should().Contain(oldTag.Id);
            createdSupplier.TagIds.Should().Contain(newTag.Id);
        }

        [Theory]
        [MemberData(nameof(ValidSupplierData))]
        public void ClearTagsFromAnExistingCustomer_ShouldSucceed(PlainSupplier supplier)
        {
            // Arrange
            Tag tag1 = new Tag("Frequent Buyer", "green");
            tag1.Id = 3;
            Tag tag2 = new Tag("Newsletter Subscriber", "purple");
            tag2.Id = 4;

            var createdSupplier = new Supplier(
              name: supplier.Name,
              corporateName: supplier.CorporateName,
              cnpj: supplier.CNPJ,
              contactPerson: supplier.ContactPerson,
              email: supplier.Email,
              phoneNumber: supplier.PhoneNumber,
              address: supplier.Address,
              notes: supplier.Notes);

            createdSupplier.AddTag(tag1.Id);
            createdSupplier.AddTag(tag2.Id);

            // Act
            createdSupplier.ClearTags();

            // Assert
            createdSupplier.TagIds.Should().BeEmpty();
        }

        public static IEnumerable<object[]> ValidSupplierData =>
            new List<object[]>
            {
                new object[]
                {
                    new PlainSupplier(
                        name: "Tech Global Distributors",
                        corporateName: "Tech Global Distribuição S.A.",
                        cnpj: "01.234.567/0001-89",
                        contactPerson: "Carlos Silva",
                        email: "carlos.silva@techglobal.com.br",
                        phoneNumber: "(11) 98765-4321",
                        address: new Address(
                            number: 500,
                            street: "Avenida Paulista",
                            city: "São Paulo",
                            state: "SP",
                            zipCode: "01311-900"
                        ),
                        notes: "Key supplier for electronic components."
                    )
                },

                new object[]
                {
                    new PlainSupplier(
                        name: "Local Fresh Market",
                        corporateName: "Farm-to-Table Produtos Ltda.",
                        cnpj: "01.234.567/0001-89",
                        contactPerson: "Maria Oliveira",
                        email: "maria@freshmarket.com",
                        phoneNumber: "(21) 5555-1234",
                        address: new Address(
                             number: 10,
                            street: "Rua do Comércio",
                            city: "Rio de Janeiro",
                            state: "RJ",
                            zipCode: "20010-000"
                        ),
                        notes: null
                    )
                },

                new object[]
                {
                    new PlainSupplier(
                        name: "Global Parts Inc.",
                        corporateName: "Global Parts Incorporated",
                        cnpj: "US123456789",
                        contactPerson: "Alice Johnson",
                        email: "alice.j@globalparts.com",
                        phoneNumber: "+1-202-555-0100",
                        address: new Address(
                             number: 15,
                            street: "Main Street",
                            city: "Springfield",
                            state: "IL",
                            zipCode: "62704"
                        ),
                        notes: "International logistics partner."
                    )
                },

                new object[]
                {
                    new PlainSupplier(
                        name: "Evil Corp",
                        corporateName: "Evil Corp",
                        cnpj: "00.000.000/0000-00",
                        contactPerson: "Mr. Robot",
                        email: "sales@evilcorp.com",
                        phoneNumber: "1234567890",
                        address: new Address(
                             number: 999,
                            street: "Dark Alley",
                            city: "Gotham",
                            state: "NJ",
                            zipCode: "07302"
                        ),
                        notes: "Highly suspicious supplier."
                    )
                }
            };

        public static IEnumerable<object[]> AddInvalidTestSuppliers =>
            new List<object[]>
            {
                new object[] {
                    new PlainSupplier(
                        name: null!, 
                        corporateName: "Valid Corp",
                        cnpj: "123",
                        contactPerson: "Jane Doe",
                        email: "test@test.com",
                        phoneNumber: "5551234",
                        address: new Address(1, "S", "C", "St", "Z"),
                        notes: "Invalid name test: null"
                    ),
                    "Supplier name cannot be empty." 
                },
                new object[] {
                    new PlainSupplier(
                        name: " ", 
                        corporateName: "Valid Corp",
                        cnpj: "123",
                        contactPerson: "Jane Doe",
                        email: "test@test.com",
                        phoneNumber: "5551234",
                        address: new Address(1, "S", "C", "St", "Z"),
                        notes: "Invalid name test: whitespace"
                    ),
                    "Supplier name cannot be empty."
                },

                new object[] {
                    new PlainSupplier(
                        name: "Valid Name",
                        corporateName: "",
                        cnpj: "123",
                        contactPerson: "Jane Doe",
                        email: "test@test.com",
                        phoneNumber: "5551234",
                        address: new Address(1, "S", "C", "St", "Z"),
                        notes: "Invalid corporate name test: empty string"
                    ),
                    "Corporate name cannot be empty."
                },

                new object[] {
                    new PlainSupplier(
                        name: "Valid Name",
                        corporateName: "Valid Corp",
                        cnpj: null!,
                        contactPerson: "Jane Doe",
                        email: "test@test.com",
                        phoneNumber: "5551234",
                        address: new Address(1, "S", "C", "St", "Z"),
                        notes: "Invalid CNPJ test: null"
                    ),
                    "CNPJ cannot be empty."
                },

                new object[] {
                    new PlainSupplier(
                        name: "Valid Name",
                        corporateName: "Valid Corp",
                        cnpj: "123",
                        contactPerson: " \t ",
                        email: "test@test.com",
                        phoneNumber: "5551234",
                        address: new Address(1, "S", "C", "St", "Z"),
                        notes: "Invalid contact person test: whitespace"
                    ),
                    "Contact person cannot be empty."
                },

                new object[] {
                    new PlainSupplier(
                        name: "Valid Name",
                        corporateName: "Valid Corp",
                        cnpj: "123",
                        contactPerson: "Jane Doe",
                        email: "",
                        phoneNumber: "5551234",
                        address: new Address(1, "S", "C", "St", "Z"),
                        notes: "Invalid email test: empty string"
                    ),
                    "Email cannot be empty."
                },

                new object[] {
                    new PlainSupplier(
                        name: "Valid Name",
                        corporateName: "Valid Corp",
                        cnpj: "123",
                        contactPerson: "Jane Doe",
                        email: "test@test.com",
                        phoneNumber: null!,
                        address: new Address(1, "S", "C", "St", "Z"),
                        notes: "Invalid phone number test: null"
                    ),
                    "Phone number cannot be empty."
                },


                new object[] {
                    new PlainSupplier(
                        name: "Valid Name",
                        corporateName: "Valid Corp",
                        cnpj: "123",
                        contactPerson: "Jane Doe",
                        email: "test@test.com",
                        phoneNumber: "5551234",
                        address: null!,
                        notes: "Invalid address test: null"
                    ),
                    "Address cannot be null."
                }
            };



        public class PlainSupplier
        {
            public string Name { get; set; }
            public string CorporateName { get; set; }
            public string CNPJ { get; set; }
            public string ContactPerson { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public Address Address { get; set; }
            public string? Notes { get; set; }
            public PlainSupplier(string name, string corporateName, string cnpj, string contactPerson, string email, string phoneNumber, Address address, string? notes = null)
            {
                Name = name;
                CorporateName = corporateName;
                CNPJ = cnpj;
                ContactPerson = contactPerson;
                Email = email;
                PhoneNumber = phoneNumber;
                Address = address;
                Notes = notes;
            }
        }
    }
}
