using Domain.Abstractions.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OrderRepository(OrderDbContext dbContext) : IOrderRepository
{
    public async Task AddAsync(Order order, CancellationToken cancellationToken = default)
    {
        await dbContext.Orders.AddAsync(order, cancellationToken);
    }

    public async Task<Order?> GetByOrderNumberAsync(Guid orderNumber, CancellationToken cancellationToken = default)
    {
        return await dbContext.Orders
            .Include(o => o.Products)
            .FirstOrDefaultAsync(o => o.OrderNumber == orderNumber, cancellationToken);
    }
    
    public async Task<bool> ExistsAsync(Guid orderNumber)
    {
        return await dbContext.Orders.AnyAsync(o => o.OrderNumber == orderNumber);
    }
}