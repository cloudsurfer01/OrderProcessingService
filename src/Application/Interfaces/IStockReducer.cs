using Application.DTOs;

namespace Application.Interfaces;

public interface IStockReducer
{
    Task ReduceAsync(IEnumerable<ProductItem> products, CancellationToken cancellationToken);
}
