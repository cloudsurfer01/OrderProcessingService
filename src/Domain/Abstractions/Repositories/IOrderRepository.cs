using Domain.Entities;

namespace Domain.Abstractions.Repositories;

public interface IOrderRepository
{
    Task AddAsync(Order order, CancellationToken cancellationToken = default);
    Task<Order?> GerByOrderNumberAsync(string orderNumber, CancellationToken cancellationToken = default);
}

