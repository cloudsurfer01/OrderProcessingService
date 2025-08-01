using Domain.ValueObjects;

namespace Domain.Tests.ValueObjects;

public class EmailTests
{
    [Fact]
    public void Constructor_Should_Create_Email_When_Valid()
    {
        var email = new Email("ikhan@paessler.com");
        Assert.Equal("ikhan@paessler.com", email.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("invalid-email")]
    [InlineData("ikhan@paessler")]
    [InlineData("ikhan@paessler,com")]
    [InlineData("ikhan@paessler..com")]
    public void Constructor_Should_Throw_Exception_When_Invalid(string param)
    {
        Assert.Throws<ArgumentException>(() => new Email(param));
    }
    
}

