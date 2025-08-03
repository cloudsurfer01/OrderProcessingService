using Domain.Entities;

namespace Domain.Abstractions.Repositories;

public interface IProductRepository
{
    Task<ProductEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task UpdateAsync(ProductEntity product, CancellationToken cancellationToken = default);
}