using Application.DTOs;
using Infrastructure.Factories;

namespace Infrastructure.Tests.Factories;

public class OrderFactoryTests
{
    [Fact]
    public void Create_Should_Return_Valid_Order_From_Request()
    {
        // Arrange
        var request = new CreateOrderRequest
        {
            Products =
            [
                new ProductItem { ProductId = "1", ProductName = "Laptop", ProductAmount = 1, ProductPrice = 1200 }
            ],
            InvoiceAddress = "123 Test Street",
            InvoiceEmailAddress = "test@example.com",
            InvoiceCreditCardNumber = "4111-1111-1111-1111"
        };

        var factory = new OrderFactory();

        // Act
        var order = factory.Create(request);

        // Assert
        Assert.NotNull(order);
        Assert.Single(order.Products);
        Assert.Equal("Laptop", order.Products[0].ProductName);
        Assert.Equal("123 Test Street", order.InvoiceAddress.Value);
        Assert.Equal("test@example.com", order.InvoiceEmailAddress.Value);
        Assert.Equal("4111-1111-1111-1111", order.CreditCardNumber.Value);
    }

    [Fact]
    public void Create_Should_Throw_When_Required_Fields_Are_Invalid()
    {
        // Arrange: missing required fields
        var request = new CreateOrderRequest
        {
            Products = [new ProductItem { ProductId = "1", ProductName = "", ProductAmount = 0, ProductPrice = -10 }],
            InvoiceAddress = "", 
            InvoiceEmailAddress = "not-an-email", 
            InvoiceCreditCardNumber = ""
        };

        var factory = new OrderFactory();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => factory.Create(request));
    }
}