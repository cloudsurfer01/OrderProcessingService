using Application.DTOs;
using Application.Orders.Validators;
using FluentValidation.TestHelper;

namespace Application.Tests.Validators;

public class CreateOrderRequestValidatorTests
{
    private readonly CreateOrderRequestValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_Required_Fields_Are_Missing()
    {
        // Arrange
        var request = new CreateOrderRequest
        {
            Products = new List<ProductItem>(), // Empty list
            InvoiceAddress = "",
            InvoiceEmailAddress = "",
            InvoiceCreditCardNumber = ""
        };

        // Act & Assert
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(r => r.InvoiceAddress);
        result.ShouldHaveValidationErrorFor(r => r.InvoiceEmailAddress);
        result.ShouldHaveValidationErrorFor(r => r.InvoiceCreditCardNumber);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Request_Is_Valid()
    {
        // Arrange
        var request = new CreateOrderRequest
        {
            Products =
            [
                new ProductItem { ProductId = "1", ProductName = "Laptop", ProductAmount = 1, ProductPrice = 999 }
            ],
            InvoiceAddress = "Valid Street 1",
            InvoiceEmailAddress = "test@example.com",
            InvoiceCreditCardNumber = "4111-1111-1111-1111"
        };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

}