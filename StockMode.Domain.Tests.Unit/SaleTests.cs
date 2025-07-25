using FluentAssertions;
using StockMode.Domain.Core.Exceptions;
using StockMode.Domain.Enums;
using StockMode.Domain.Sales;

namespace StockMode.Domain.Tests.Unit
{
    public class SaleTests
    {
        /// <summary>
        /// Tests for the constructor of the Sale Aggregate Root.
        /// </summary>
        [Fact]
        public void CreateSale_ShouldSucceed_WhenPaymentMethodIsValid()
        {
            // Arrange 
            var sale = new Sale(PaymentMethod.Pix);

            // Assert
            sale.Should().NotBeNull();
            sale.PaymentMethod.Should().Be(PaymentMethod.Pix);
            sale.SaleDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Fact]
        public void CreateSale_ShouldThrowDomainException_WhenPaymentMethodIsInvalid()
        {
            // Arrange
            Action act = () => new Sale((PaymentMethod)999);

            // Assert

            act.Should().Throw<DomainException>()
                .WithMessage("Invalid payment method.");
        }

        /// <summary>
        /// Tests for the AddItem method of the Sale Aggregate Root.
        /// </summary>
        [Fact]
        public void AddItem_ShouldAddItemToSale_WhenNewItemIsValid()
        {
            // Arrange
            var sale = new Sale(PaymentMethod.Pix);
            var saleItem = new SaleItem(variationId: 1, quantity: 2, priceAtSale: 100m);

            // Act
            sale.AddItem(saleItem);

            // Assert
            sale.Items.Should().ContainSingle();
            sale.Items.First().VariationId.Should().Be(1);
            sale.Items.First().Quantity.Should().Be(2);
            sale.Items.First().PriceAtSale.Should().Be(100m);
        }

        [Fact]
        public void AddItem_ShouldThrowDomainException_WhenSaleIsNotPending()
        {
            // Arrange
            var sale = new Sale(PaymentMethod.Pix);
            var saleItem = new SaleItem(variationId: 1, quantity: 2, priceAtSale: 100m);
            sale.CancelSale();
            // Act
            Action act = () => sale.AddItem(saleItem);

            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage("Cannot add items to a sale that is not pending.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void AddItem_ShouldThrowDomainException_WhenItemQuantityIsZeroOrNegative(int quantity)
        {
            // Arrange
            var sale = new Sale(PaymentMethod.Pix);

            // Act
            Action act = () => new SaleItem(variationId: 1, quantity: quantity, priceAtSale: 100m);

            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage("Sale Item quantity must be greater than zero.");
        }

        /// <summary>
        /// Tests for the ApplyDiscount method of the Sale Aggregate Root.
        /// </summary>
        [Fact]
        public void ApplyDiscount_ShouldApplyDiscount_WhenDetailsAreValid()
        {
            // Arrange
            var sale = new Sale(PaymentMethod.Pix);
            var saleItem = new SaleItem(variationId: 1, quantity: 2, priceAtSale: 100m);
            sale.AddItem(saleItem);

            // Act
            sale.ApplyDiscount(20m);

            // Assert
            sale.Discount.Should().Be(20m);
            sale.FinalPrice.Should().Be(180m);
        }

        [Fact]
        public void ApplyDiscount_ShouldThrowDomainException_WhenSaleIsNotPending()
        {
            // Arrange
            var sale = new Sale(PaymentMethod.Pix);
            var saleItem = new SaleItem(variationId: 1, quantity: 2, priceAtSale: 100m);
            sale.AddItem(saleItem);
            sale.CancelSale();
            // Act
            Action act = () => sale.ApplyDiscount(20m);
            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage("Cannot apply a discount to a sale that is not pending.");
        }

        [Fact]
        public void ApplyDiscount_ShouldThrowDomainException_WhenDiscountIsNegative()
        {
            // Arrange
            var sale = new Sale(PaymentMethod.Pix);
            var saleItem = new SaleItem(variationId: 1, quantity: 2, priceAtSale: 100m);
            sale.AddItem(saleItem);

            // Act
            Action act = () => sale.ApplyDiscount(-10m);

            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage("Discount cannot be negative.");
        }

        [Fact]
        public void ApplyDiscount_ShouldThrowDomainException_WhenDiscountExceedsTotalPrice()
        {
            // Arrange
            var sale = new Sale(PaymentMethod.Pix);
            var saleItem = new SaleItem(variationId: 1, quantity: 2, priceAtSale: 100m);
            sale.AddItem(saleItem);

            // Act
            Action act = () => sale.ApplyDiscount(250m);

            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage("Discount cannot be greater than the total price of the sale.");
        }
        /// <summary>
        /// Tests for the ChangePaymentMethod method of the Sale Aggregate Root.
        /// </summary>
        [Fact]
        public void ChangePaymentMethod_ShouldChangePaymentMethod_WhenNewMethodIsValid()
        {
            // Arrange
            var sale = new Sale(PaymentMethod.Pix);

            // Act
            sale.ChangePaymentMethod(PaymentMethod.CreditCard);

            // Assert
            sale.PaymentMethod.Should().Be(PaymentMethod.CreditCard);
        }

        [Fact]
        public void ChangePaymentMethod_ShouldThrowDomainException_WhenNewPaymentMethodIsInvalid()
        {
            // Arrange
            var sale = new Sale(PaymentMethod.Pix);
            // Act
            Action act = () => sale.ChangePaymentMethod((PaymentMethod)999);
            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage("Invalid payment method.");
        }

        [Fact]
        public void ChangePaymentMethod_ShouldThrowDomainExcpetion_WhenSaleIsNotPending()
        {
            // Arrange
            var sale = new Sale(PaymentMethod.Pix);
            sale.CancelSale();
            // Act
            Action act = () => sale.ChangePaymentMethod(PaymentMethod.CreditCard);
            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage("Cannot change payment method for a sale that is not pending.");
        }

        /// <summary>
        /// Tests for the CompleteSale method of the Sale Aggregate Root.
        /// </summary>
        [Fact]
        public void CompleteSale_ShouldCompleteSale_WhenDetailsAreValid()
        {
            // Arrange
            var sale = new Sale(PaymentMethod.Pix);
            var saleItem = new SaleItem(variationId: 1, quantity: 2, priceAtSale: 100m);
            sale.AddItem(saleItem);

            // Act
            sale.CompleteSale();

            // Assert
            sale.Status.Should().Be(SaleStatus.Completed);
        }

        [Fact]
        public void CompleteSale_ShouldThrowDomainException_WhenSaleIsNotPending()
        {
            // Arrange
            var sale = new Sale(PaymentMethod.Pix);
            sale.CancelSale();

            // Act
            Action act = () => sale.CompleteSale();

            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage("Only a pending sale can be completed.");
        }

        [Fact]
        public void CompleteSale_ShouldThrowDomainException_WhenNoItemsInSale()
        {
            // Arrange
            var sale = new Sale(PaymentMethod.Pix);

            // Act
            Action act = () => sale.CompleteSale();

            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage("Cannot complete a sale with no items.");
        }

        /// <summary>
        /// Tests for the CancelSale method of the Sale Aggregate Root.
        /// </summary>
        [Fact]
        public void CancelSale_ShouldSetStatusToCancelled_WhenCalled()
        {
            // Arrange
            var sale = new Sale(PaymentMethod.Pix);

            // Act
            sale.CancelSale();

            // Assert
            sale.Status.Should().Be(SaleStatus.Cancelled);
        }

        [Fact]
        public void CancelSale_ShouldThrowDomainException_WhenSaleIsAlreadyCancelled()
        {
            // Arrange
            var sale = new Sale(PaymentMethod.Pix);
            sale.CancelSale();
            // Act
            Action act = () => sale.CancelSale();
            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage("Sale has already been cancelled.");
        }

        [Fact]
        public void CancelSale_ShouldThrowDomainException_WhenSaleIsAlreadyCompleted()
        {
            // Arrange
            var sale = new Sale(PaymentMethod.Pix);
            var saleItem = new SaleItem(variationId: 1, quantity: 2, priceAtSale: 100m);
            sale.AddItem(saleItem);
            sale.CompleteSale();
            // Act
            Action act = () => sale.CancelSale();
            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage("Cannot cancel a sale that has already been completed.");
        }

        /// <summary>
        /// Tests for the constructor of the SaleItem entity.
        /// </summary>
        [Fact]
        public void CreateSaleItem_ShouldSucceed_WhenDetailsAreValid()
        {
            // Arrange
            int variationId = 1;
            int quantity = 2;
            decimal priceAtSale = 100m;

            // Act
            var saleItem = new SaleItem(variationId, quantity, priceAtSale);

            // Assert
            saleItem.VariationId.Should().Be(variationId);
            saleItem.Quantity.Should().Be(quantity);
            saleItem.PriceAtSale.Should().Be(priceAtSale);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CreteSaleItem_ShouldThrowDomainException_WhenVariationIdIsInvalid(int variationId)
        {
            // Arrange
            int quantity = 2;
            decimal priceAtSale = 100m;
            // Act
            Action act = () => new SaleItem(variationId, quantity, priceAtSale);
            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage("Invalid Variation ID provided for Sale Item.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CreateSaleItem_ShouldThrowDomainException_WhenQuantityIsZeroOrNegative(int quantity)
        {
            // Arrange
            int variationId = 1;
            decimal priceAtSale = 100m;

            // Act
            Action act = () => new SaleItem(variationId, quantity, priceAtSale);

            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage("Sale Item quantity must be greater than zero.");
        }

        [Fact]
        public void CreateSaleItem_ShouldThrowDomainException_WhenPriceAtSaleIsNegative()
        {
            // Arrange
            int variationId = 1;
            int quantity = 2;
            decimal priceAtSale = -100m;

            // Act
            Action act = () => new SaleItem(variationId, quantity, priceAtSale);

            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage("Sale Item price cannot be negative.");
        }

        /// <summary>
        /// Tests for the IncreaseQuantity method of the SaleItem entity.
        /// </summary>
        [Fact]
        public void IncreaseQuantity_ShouldIncreaseItemQuantity_WhenAdditionalQuantityIsValid()
        {
            // Arrange
            var saleItem = new SaleItem(variationId: 1, quantity: 2, priceAtSale: 100m);
            int additionalQuantity = 3;

            // Act
            saleItem.IncreaseQuantity(additionalQuantity);

            // Assert
            saleItem.Quantity.Should().Be(5);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void IncreaseQuantity_ShouldThrowDomainException_WhenAdditionalQuantityIsZeroOrNegative(int additionalQuantity)
        {
            // Arrange
            var saleItem = new SaleItem(variationId: 1, quantity: 2, priceAtSale: 100m);

            // Act
            Action act = () => saleItem.IncreaseQuantity(additionalQuantity);

            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage("Quantity to add must be greater than zero.");
        }
    }
}
