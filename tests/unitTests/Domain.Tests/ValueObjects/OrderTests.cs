using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Tests.ValueObjects;

public class OrderTests
{
    [Fact]
    public void Constructor_Should_Create_Order_When_Valid()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product("123", "Laptop", 2, 1499.99m),
            new Product("456", "Mouse", 1, 49.99m)
        };

        var address = new Address("123 Sample Street, 90402 Berlin");
        var email = new Email("ikhan@paessler.com");
        var creditCard = new CreditCardNumber("4111-1111-1111-1111");

        // Act
        var order = new Order(products, address, email, creditCard);

        // Assert
        Assert.NotEqual(Guid.Empty, order.OrderNumber);
        Assert.Equal(2, order.Products.Count);
        Assert.Equal(address, order.InvoiceAddress);
        Assert.Equal(email, order.InvoiceEmailAddress);
        Assert.Equal(creditCard, order.CreditCardNumber);
        Assert.True((DateTime.UtcNow - order.CreatedAt).TotalSeconds < 5);
    }

    [Fact]
    public void Constructor_Should_Throw_When_Product_Is_Empty()
    {
        // Arrange
        var product = new List<Product>();
        var address = new Address("123 Sample Street, 90402 Berlin");
        var email = new Email("ikhan@paessler.com");
        var creditCard = new CreditCardNumber("4111-1111-1111-1111");

        // Assert
        Assert.Throws<ArgumentException>(() => new Order(product, address, email, creditCard));
    }

    [Fact]
    public void TotalPrice_Should_Return_Sum_Of_Product_TotalPrices()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product("123", "Laptop", 2, 100m),
            new Product("456", "Mouse", 1, 50m)
        };

        // Act
        var order = new Order(products, null, null, null);

        var totalPrice = order.TotalPrice();

        // Assert
        Assert.Equal(250m, totalPrice);
    }

    [Fact]
    public void Products_Should_Be_ReadOnly()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product("123", "Laptop", 1, 100m)
        };

        var order = new Order(products, null, null, null);

        //Act & Assert
        Assert.IsAssignableFrom<IReadOnlyList<Product>>(order.Products);
        Assert.Throws<NotSupportedException>(() => ((IList<Product>)order.Products).Add(new Product("456", "Mouse", 1, 50m)));
    }
}