using Application.DTOs;
using Domain.Exceptions;
using Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace Infrastructure.Tests.Services;

public class StockServiceTests
{
    [Fact]
    public async Task CheckAvailabilityAsync_Should_Complete_When_Stock_Is_Sufficient()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<StockService>>();
        var service = new StockService(mockLogger.Object);

        var items = new List<ProductItem>
        {
            new ProductItem { ProductId = "1", ProductName = "Keyboard", ProductAmount = 3, ProductPrice = 100 }
        };

        // Act & Assert
        await service.CheckAvailabilityAsync(items, CancellationToken.None);
    }

    [Fact]
    public async Task CheckAvailabilityAsync_Should_Throw_When_Stock_Exceeds_Limit()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<StockService>>();
        var service = new StockService(mockLogger.Object);

        var items = new List<ProductItem>
        {
            new ProductItem { ProductId = "2", ProductName = "Monitor", ProductAmount = 6, ProductPrice = 200 }
        };

        // Act & Assert
        await Assert.ThrowsAsync<OutOfStockException>(() =>
            service.CheckAvailabilityAsync(items, CancellationToken.None));
    }
}
