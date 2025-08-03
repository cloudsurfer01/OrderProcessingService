using Domain.Abstractions.Repositories;

namespace Infrastructure.Services;

public class ProductStockChecker(IProductRepository productRepository) : IProductStockChecker
{
    public async Task<bool> IsProductInStockAsync(string productId, int quantity, CancellationToken cancellationToken = default)
    {
        if (!Guid.TryParse(productId, out var guid))
            return false;

        var product = await productRepository.GetByIdAsync(guid, cancellationToken);

        return product?.IsInStock(quantity) == true;
    }
}