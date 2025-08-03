using System.Runtime.InteropServices.Marshalling;
using Domain.Abstractions.Repositories;

namespace Infrastructure.Services;
public class ProductStockChecker : IProductStockChecker
{
    public async Task<bool> IsProductInStockAsync(string productId, int quantity, CancellationToken cancellationToken = default)
    {
        return true;
    }
}
