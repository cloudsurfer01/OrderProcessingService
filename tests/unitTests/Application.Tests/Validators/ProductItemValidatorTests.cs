using Application.DTOs;
using Application.Orders.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace Application.Tests.Validators;

public class ProductItemValidatorTests
{
    private readonly ProductItemValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_ProductItem_Is_Invalid()
    {
        // Arrange
        var item = new ProductItem
        {
            ProductId = "",
            ProductName = "",
            ProductAmount = 0,
            ProductPrice = 0
        };

        // Act
        var result = _validator.TestValidate(item);

        // Assert
        result.ShouldHaveValidationErrorFor(i => i.ProductId);
        result.ShouldHaveValidationErrorFor(i => i.ProductName);
        result.ShouldHaveValidationErrorFor(i => i.ProductAmount);
        result.ShouldHaveValidationErrorFor(i => i.ProductPrice);
    }

    [Fact]
    public void Should_Not_Have_Error_When_ProductItem_Is_Valid()
    {
        // Arrange
        var item = new ProductItem
        {
            ProductId = "123",
            ProductName = "Phone",
            ProductAmount = 2,
            ProductPrice = 499.99m
        };

        // Act
        var result = _validator.TestValidate(item);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

}