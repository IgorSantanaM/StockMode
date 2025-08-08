using FluentAssertions;
using StockMode.Domain.Core.Exceptions;
using StockMode.Domain.Enums;
using StockMode.Domain.StockMovements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Domain.Tests.Unit
{
    public class StockMovementTests
    {
        /// <summary>
        /// Tests the creation of a StockMovement for a sale.
        /// </summary>
        [Fact]
        public void CreateForSale_ShouldCreateStockMovement_WithValidParameters()
        {
            // Arrange
            int variationId = 1;
            int quantitySold = 5;
            int stockAfter = 10;
            int saleId = 100;
            int customerId = 1;

            // Act
            var stockMovement = StockMovement.CreateForSale(variationId, quantitySold, stockAfter, saleId, customerId);

            // Assert
            Assert.NotNull(stockMovement);
            Assert.Equal(variationId, stockMovement.VariationId);
            Assert.Equal(-quantitySold, stockMovement.Quantity);
            Assert.Equal(StockMovementType.SaleExit, stockMovement.Type);
            Assert.Equal(stockAfter, stockMovement.StockAfterMovement);
            Assert.Equal(saleId, stockMovement.SaleId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CreateForSale_ShouldThrowException_WhenVariationIdIsNegativeOrZero(int variationId)
        {
            // Arrange 
            int quantitySold = 5;
            int stockAfter = 10;
            int saleId = 100;
            int customerId = 1;

            // Act
            Action act = () => StockMovement.CreateForSale(variationId, quantitySold, stockAfter, saleId, customerId);

            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage("Stock Movement must be associated with a valid product variation.");
        }

        [Fact]
        public void CreateForSale_ShouldThrowException_WhenQuantitySoldIsZero()
        {
            // Arrange
            int variationId = 1;
            int quantitySold = 0; 
            int stockAfter = 10;
            int saleId = 100;
            int customerId = 1;

            // Act
            Action act = () => StockMovement.CreateForSale(variationId, quantitySold, stockAfter, saleId, customerId);

            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage("Stock Movement quantity cannot be zero.");
        }

        /// <summary>
        /// Tests the creation of a StockMovement for a purchase entry.
        /// </summary>
        [Fact]
        public void CreateForPurchase_ShouldCreateStockMovement_WhenparametersAreValid()
        {
            // Arrange
            int variationId = 1;
            int quantityReceived = 5;
            int stockAfter = 10;
            int supplierId = 1;

            // Act
            var stockMovement = StockMovement.CreateForPurchase(variationId, quantityReceived, stockAfter, supplierId);

            // Assert
            Assert.NotNull(stockMovement);
            Assert.Equal(variationId, stockMovement.VariationId);
            Assert.Equal(quantityReceived, stockMovement.Quantity);
            Assert.Equal(StockMovementType.PurchaseEntry, stockMovement.Type);
            Assert.Equal(stockAfter, stockMovement.StockAfterMovement);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CreateForPurchase_ShouldThrowDomainException_WhenVariationIdIsZeroOrNegative(int variationId)
        {
            // Arrange
            int quantityReceived = 5;
            int stockAfter = 10;
            int supplierId = 1;

            // Act
            Action act = () => StockMovement.CreateForPurchase(variationId, quantityReceived, stockAfter, supplierId);

            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage("Stock Movement must be associated with a valid product variation.");
        }

        [Fact]
        public void CreateForPurchase_ShouldThrowDomainException_WhenQuantityReceivedIsZero()
        {
            // Arrange
            int variationId = 1;
            int quantityReceived = 0;
            int stockAfter = 10;
            int supplierId = 1;
            // Act
            Action act = () => StockMovement.CreateForPurchase(variationId, quantityReceived, stockAfter, supplierId);

            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage("Stock Movement quantity cannot be zero.");
        }

        /// <summary>
        /// Tests the creation of a StockMovement for a positive adjustment.
        /// </summary>
        [Fact]
        public void CreateForAdjustment_ShouldCreateStockMovement_WhenParametersAreValid()
        {
            // Arrange
            int variationId = 1;
            int quantityAdjusted = -3;
            int stockAfter = 10;
            string reason = "Inventory correction";
            int customerId = 1;

            // Act
            var stockMovement = StockMovement.CreateForAdjustment(variationId, quantityAdjusted, stockAfter, reason, customerId);

            // Assert
            Assert.NotNull(stockMovement);
            Assert.Equal(variationId, stockMovement.VariationId);
            Assert.Equal(quantityAdjusted, stockMovement.Quantity);
            Assert.Equal(StockMovementType.LossAdjustment, stockMovement.Type);
            Assert.Equal(stockAfter, stockMovement.StockAfterMovement);
            Assert.Equal(reason, stockMovement.Observation);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CreateForAdjustment_ShouldThrowDomainException_WhenVariationIdIsZeroOrNegative(int variationId)
        {
            // Arrange
            int quantityAdjusted = -3;
            int stockAfter = 10;
            string reason = "Inventory correction";
            int customerId = 1;

            // Act
            Action act = () => StockMovement.CreateForAdjustment(variationId, quantityAdjusted, stockAfter, reason, customerId);

            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage("Stock Movement must be associated with a valid product variation.");
        }

        [Fact]
        public void CreateForAdjustment_ShouldThrowDomainException_WhenQuantityAdjustedIsZero()
        {
            // Arrange
            int variationId = 1;
            int quantityAdjusted = 0;
            int stockAfter = 10;
            string reason = "Inventory correction";
            int customerId = 1;

            // Act
            Action act = () => StockMovement.CreateForAdjustment(variationId, quantityAdjusted, stockAfter, reason, customerId);
            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage("Stock Movement quantity cannot be zero.");
        }
    }
}
