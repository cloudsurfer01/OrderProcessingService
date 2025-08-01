using Domain.ValueObjects;
using Xunit;

namespace Domain.Tests.ValueObjects;

public class CreditCardNumberTests
{
    [Fact]
    public void Constructor_Should_Create_CreditCardNumber_When_Valid()
    {
        var card = new CreditCardNumber("1234-5678-9012-3456");

        Assert.Equal("1234567890123456", card.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("1234")]
    [InlineData("123456789012345678901")]
    public void Constructor_Should_Throw_When_Invalid(string input)
    {
        Assert.Throws<ArgumentException>(() => new CreditCardNumber(input));
    }
}