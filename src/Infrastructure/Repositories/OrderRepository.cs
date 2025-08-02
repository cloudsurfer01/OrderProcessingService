using Infrastructure.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class OrderRepository(OrderDbContext dbContext) : IOrderRepository
{
    public async Task<Order?> GetByOrderNumberAsync(Guid orderNumber)
    {
        return await dbContext.Orders
            .Include(o => o.Products)
            .FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);
    }

    public async Task AddAsync(Order order)
    {
        await dbContext.Orders.AddAsync(order);
    }

    public async Task<bool> ExistsAsync(Guid orderNumber)
    {
        return await dbContext.Orders.AnyAsync(o => o.OrderNumber == orderNumber);
    }
}
