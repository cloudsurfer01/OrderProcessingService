using Domain.Entities;

namespace Domain.Abstractions.Repositories;

public interface IOrderRepository
{
    Task AddAsync(Order order, CancellationToken cancellationToken = default);
    Task<Order?> GetByOrderNumberAsync(Guid orderNumber, CancellationToken cancellationToken = default);
}

