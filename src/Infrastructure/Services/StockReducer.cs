using Application.DTOs;
using Application.Interfaces;
using Domain.Abstractions.Repositories;

namespace Infrastructure.Services;

public class StockReducer(IProductRepository productRepository) : IStockReducer
{
    public async Task ReduceAsync(IEnumerable<ProductItem> products, CancellationToken cancellationToken)
    {
        foreach (var item in products)
        {
            await productRepository.ReduceStockAsync(Guid.Parse(item.ProductId), item.ProductAmount, cancellationToken);
        }
    }
}

