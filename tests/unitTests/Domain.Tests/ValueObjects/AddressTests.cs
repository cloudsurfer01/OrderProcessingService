using Domain.ValueObjects;

namespace Domain.Tests.ValueObjects;

public class AddressTests
{
    [Fact]
    public void Constructor_Should_Create_Address_When_Valid()
    {
        var address = new Address("123 Sample Street, 90402 Berlin");

        Assert.Equal("123 Sample Street, 90402 Berlin", address.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_Should_Throw_When_Invalid(string input)
    {
        Assert.Throws<ArgumentException>(() => new Address(input));
    }
}

