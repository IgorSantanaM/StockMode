using FluentAssertions;
using StockMode.Domain.Enums;
using StockMode.Domain.Sales;

namespace StockMode.Domain.Tests.Unit
{
    public class SaleTests
    {
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


    }
}
