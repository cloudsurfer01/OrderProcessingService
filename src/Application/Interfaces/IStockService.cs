using Application.DTOs;

namespace Application.Interfaces;

public interface IStockService
{
    Task CheckAvailabilityAsync(IEnumerable<ProductItem> items, CancellationToken cancellationToken);
}
