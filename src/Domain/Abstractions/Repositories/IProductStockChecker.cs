
namespace Domain.Abstractions.Repositories;

public interface IProductStockChecker
{
    Task<bool> IsProductInStockAsync(string productId, int quantity, CancellationToken cancellationToken = default);
}

