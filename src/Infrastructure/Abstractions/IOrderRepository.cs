using Domain.Entities;

namespace Infrastructure.Abstractions;
public interface IOrderRepository
{
    Task<Order?> GetByOrderNumberAsync(Guid orderNumber);
    Task AddAsync(Order order);
    Task<bool> ExistsAsync(Guid orderNumber);
}

