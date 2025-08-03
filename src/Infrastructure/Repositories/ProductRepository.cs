using Domain.Abstractions.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository(OrderDbContext context) : IProductRepository
{
    public async Task<ProductEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Products.FindAsync([id], cancellationToken);
    }

    //public async Task UpdateAsync(ProductEntity product, CancellationToken cancellationToken = default)
    //{
    //    context.Products.Update(product);
    //    await context.SaveChangesAsync(cancellationToken);
    //}

    public async Task ReduceStockAsync(Guid productId, int quantity, CancellationToken cancellationToken = default)
    {
        var product = await context.Products.FindAsync([productId], cancellationToken);

        if (product is null)
            throw new InvalidOperationException($"Product {productId} not found.");

        product.ReduceStock(quantity);
        context.Products.Update(product);

        await context.SaveChangesAsync(cancellationToken);
    }
}
