using Domain.ValueObjects;

namespace Domain.Tests.ValueObjects;

public class ProductTests
{
    [Fact]
    public void Constructor_Should_Create_Product_When_Valid()
    {
        var product = new Product("123", "Laptop", 2, 1499.99m);

        Assert.Equal("123", product.ProductId);
        Assert.Equal("Laptop", product.ProductName);
        Assert.Equal(2, product.ProductAmount);
        Assert.Equal(1499.99m, product.ProductPrice);
        Assert.Equal(2999.98m, product.TotalPrice());
    }

    [Theory]
    [InlineData("", "Laptop", 1, 1000)]
    [InlineData("123", "", 1, 1000)]
    [InlineData("123", "Laptop", 0, 1000)]
    [InlineData("123", "Laptop", 1, -50)]
    public void Constructor_Should_Throw_Exception_When_Invalid(string id, string name, int amount, decimal price)
    {
        Assert.Throws<ArgumentException>(() => new Product(id, name, amount, price));
    }
}
