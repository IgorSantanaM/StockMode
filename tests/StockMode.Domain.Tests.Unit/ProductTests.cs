using FluentAssertions;
using StockMode.Domain.Core.Exceptions;
using StockMode.Domain.Products;

namespace StockMode.Domain.Tests.Unit
{
    public class ProductTests
    {
        /// <summary>
        /// Tests for the constructor of the Product Aggregate Root.
        /// </summary>
        [Fact]
        public void CreateProduct_ShouldSucceed_WhenNameIsValid()
        {
            // Arrange
            var productName = "Valid Product Name";
            var productDescription = "Valid product description";

            // Act
            var product = new Product(productName, productDescription);

            // Assert
            product.Should().NotBeNull();
            product.Name.Should().Be(productName);
            product.Description.Should().Be(productDescription);
            product.IsActive.Should().BeTrue();
            product.Variations.Should().BeEmpty();
        }
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void CreateProduct_ShouldThrowDomainException_WhenNameIsEmpty(string invalidName)
        {
            // Arrange
            Action act = () => new Product(invalidName, "Valid description");

            // Act & Assert
            act.Should().Throw<DomainException>()
                .WithMessage("Product name cannot be empty.");
        }

        /// <summary>
        /// Tests for the AddVariation method of the Product Aggregate Root.
        /// </summary>
        [Fact]
        public void Addvariation_ShouldAddVariationToList_WhenProductIsActivated()
        {
            // Arrange
            var product = new Product("Test Product", "Test Description");

            // Act
            product.AddVariation("Test Variation", "TEST-SKU-001", 10.00m, 15.00m, 100);

            // Assert
            product.Should().NotBeNull();
            product.Variations.Should().NotBeNull();
            product.Variations.Should().HaveCount(1);
        }

        [Fact]
        public void AddVariation_ShouldThrowDomainException_WhenSkuIsDuplicated()
        {
            // Arrange
            var product = new Product("Test Product", "Test Description");
            var sku = "SKU-123";
            product.AddVariation("variation 1", sku, 10.00m, 15.00m, 100);

            // Act
            Action act = () => product.AddVariation("variation 2", sku, 12.00m, 18.00m, 50);

            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage($"A variation with SKU '{sku}' already exists for this product.");
        }

        [Fact]
        public void AddVariation_ShouldThrowDomainException_WhenProductIsInactivated()
        {
            // Arrange
            var product = new Product("Test Product", "Test Description");
            product.Deactivate();
            // Act
            Action act = () => product.AddVariation("Test Variation", "TEST-SKU-001", 10.00m, 15.00m, 100);

            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage("Cannot add a variation to an inactive product.");
        }

        /// <summary>
        /// Tests for the UpdateDetails method of the Product Aggregate Root.
        /// </summary>
        [Fact]
        public void UpdateDetails_ShouldUpdateProductDetails_WhenNewNameIsValid()
        {
            // Arrange
            var product = new Product("Old Name", "Old Description");
            var newName = "New Valid Name";
            var newDescription = "New Valid Description";
            // Act
            product.UpdateDetails(newName, newDescription);

            // Assert
            product.Should().NotBeNull();
            product.Name.Should().Be(newName);
            product.Description.Should().Be(newDescription);
            product.IsActive.Should().BeTrue();
            product.Variations.Should().BeEmpty();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void UpdateDetails_ShouldThrowDomainException_WhenNewNameIsEmpty(string invalidName)
        {
            // Arrange
            var product = new Product("Old Name", "Old Description");

            // Act
            Action act = () => product.UpdateDetails(invalidName, "New Description");

            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage("Product name cannot be empty.");
        }

        /// <summary>
        /// Tests for the Activate method of the Product Aggregate Root.
        /// </summary>
        [Fact]
        public void ActivateProduct_ShouldActivateProduct_WhenVariationsExist()
        {
            // Arrange
            var product = new Product("Test Product", "Test Description");
            product.AddVariation("Test Variation", "TEST-SKU-001", 10.00m, 15.00m, 100);

            // Act
            product.Activate();

            // Assert
            product.Should().NotBeNull();
            product.IsActive.Should().BeTrue();
        }

        [Fact]
        public void ActivateProduct_ShouldThrowDomainException_WhenNoVariationsExist()
        {
            // Arrange
            var product = new Product("Test Product", "Test Description");

            // Act
            Action act = () => product.Activate();

            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage("Cannot activate a product with no variations.");
        }

        /// <summary>
        /// Tests for the DecreaseStock method of the Variation Entity.
        /// </summary>
        [Fact]
        public void DecreaseStock_ShouldDecreaseStock_WhenQuantityIsValid()
        {
            // Arrange
            var product = new Product("Test Product", "Test Description");
            product.AddVariation("Test Variation", "TEST-SKU-001", 10.00m, 15.00m, 100);
            var variation = product.Variations.First();
            // Act
            variation.DecreaseStock(20);
            // Assert
            variation.StockQuantity.Should().Be(80);
        }

        [Fact]
        public void DecreaseStock_ShouldThrowDomainException_WhenQuantityIsNegative()
        {
            // Arrange
            var product = new Product("Test Product", "Test Description");
            product.AddVariation("Test Variation", "TEST-SKU-001", 10.00m, 15.00m, 100);
            var variation = product.Variations.First();
            // Act
            Action act = () => variation.DecreaseStock(-10);
            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage("Quantity to decrease must be positive.");
        }

        [Fact]
        public void DecreaseStock_ShouldThrowDomainException_WhenNotEnoughStock()
        {
            // Arrange
            var product = new Product("Test Product", "Test Description");
            product.AddVariation("Test Variation", "TEST-SKU-001", 10.00m, 15.00m, 100);
            var variation = product.Variations.First();
            var requestsdQuantity = 150;
            // Act
            Action act = () => variation.DecreaseStock(requestsdQuantity);
            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage($"Not enough stock for SKU '{variation.Sku}'. Available: {variation.StockQuantity}, Requested: {requestsdQuantity}.");
        }

        /// <summary>
        /// Tests for the IncreaseStock method of the Variation Entity.
        /// </summary>
        [Fact]
        public void IncreaseStock_ShouldIncreaseStock_WhenQuantityIsValid()
        {
            // Arrange
            var product = new Product("Test Product", "Test Description");
            product.AddVariation("Test Variation", "TEST-SKU-001", 10.00m, 15.00m, 100);
            var variation = product.Variations.First();
            // Act
            variation.IncreaseStock(50);
            // Assert
            variation.StockQuantity.Should().Be(150);
        }

        [Fact]
        public void IncreaseStock_ShouldThrowDomainException_WhenQuantityIsNegative()
        {
            // Arrange
            var product = new Product("Test Product", "Test Description");
            product.AddVariation("Test Variation", "TEST-SKU-001", 10.00m, 15.00m, 100);
            var variation = product.Variations.First();
            // Act
            Action act = () => variation.IncreaseStock(-10);
            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage("Quantity to increase must be positive.");
        }

        public void UpdateVariation_ShouldUpdateVariationDetails_WhenDetailsAreValid()
        {
            // Arrange
            var product = new Product("Test Product", "Test Description");
            product.AddVariation("Test Variation", "TEST-SKU-001", 10.00m, 15.00m, 100);
            var variation = product.Variations.First();
            var newName = "Updated Variation Name";
            var newSku = "UPDATED-SKU-001";
            var newCostPrice = 12.00m;
            var newSalePrice = 18.00m;
            var newStock = 200;

            // Act
            variation.Update(newName, newSku, newCostPrice, newSalePrice, newStock);

            // Assert
            variation.Name.Should().Be(newName);
            variation.Sku.Should().Be(newSku);
            variation.CostPrice.Should().Be(newCostPrice);
            variation.SalePrice.Should().Be(newSalePrice);
            variation.StockQuantity.Should().Be(newStock);
        }
        [Theory]
        [InlineData("", "TEST-SKU-001", 10.00, 15.00, 100)]
        [InlineData(" ", "TEST-SKU-001", 10.00, 15.00, 100)]
        [InlineData("Updated Variation", "", 10.00, 15.00, 100)]
        [InlineData("Updated Variation", " ", 10.00, 15.00, 100)]
        [InlineData("Updated Variation", "TEST-SKU-001", -5.00, 15.00, 100)]
        [InlineData("Updated Variation", "TEST-SKU-001", 10.00, -5.00, 100)]
        [InlineData("Updated Variation", "TEST-SKU-001", 10.00, 15.00, -50)]
        public void UpdateVariation_ShouldThrowDomainException_WhenDetailsAreInvalid(string name, string sku, decimal costPrice, decimal salePrice, int stock)
        {
            // Arrange
            var product = new Product("Test Product", "Test Description");
            product.AddVariation("valid name", "valid-sku", 10.00m, 15.00m, 100);
            var variation = product.Variations.First();

            // Act
            Action act = () => variation.Update(name, sku, costPrice, salePrice, stock);

            // Assrt
            act.Should().Throw<DomainException>();
        }
    }
}
