using Application.DTOs;
using Domain.Abstractions.Repositories;
using MediatR;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using Application.Interfaces;

namespace Application.Orders.Commands;

public class CreateOrderCommandHandler(
    IOrderRepository orderRepository,
    IProductStockChecker productStockChecker,
    IProductRepository productRepository,
    IOrderFactory orderFactory,
    IStockService stockService,
    IStockReducer stockReducer,
    ILogger<CreateOrderCommandHandler> logger
) : IRequestHandler<CreateOrderCommand, Guid> // Changed return type
{
    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        // Check stock availability before creating the order
        await stockService.CheckAvailabilityAsync(request.Request.Products, cancellationToken);

        // Create the order using factory
        var order = orderFactory.Create(request.Request);

        // Persist the order
        await orderRepository.AddAsync(order, cancellationToken);
        logger.LogInformation("Order {OrderNumber} created successfully.", order.OrderNumber);

        // Reduce stock for ordered products
        await stockReducer.ReduceAsync(request.Request.Products, cancellationToken);

        // Return only the OrderNumber (Guid)
        return order.OrderNumber;
    }
}