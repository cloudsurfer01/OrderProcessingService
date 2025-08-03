using Application.DTOs;
using Application.Interfaces;
using Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class StockService(ILogger<StockService> logger) : IStockService
{
    public Task CheckAvailabilityAsync(IEnumerable<ProductItem> items, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        foreach (var item in items)
        {
            if (item.ProductAmount > 5)
            {
                logger.LogWarning("Product {ProductId} is out of stock", item.ProductId);
                throw new OutOfStockException(item.ProductName);
            }
        }

        return Task.CompletedTask;
    }
}