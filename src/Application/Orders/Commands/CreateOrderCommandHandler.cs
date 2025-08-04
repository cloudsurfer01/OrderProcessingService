using Application.DTOs;
using Application.Interfaces;
using Domain.Abstractions.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Orders.Commands;

public class CreateOrderCommandHandler(
    IOrderRepository orderRepository,
    IProductStockChecker productStockChecker,
    IProductRepository productRepository,
    IOrderFactory orderFactory,
    IStockService stockService,
    IOrderResponseMapper orderResponseMapper,
    ILogger<CreateOrderCommandHandler> logger
        ) : IRequestHandler<CreateOrderCommand, OrderResponse>
{
    public async Task<OrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        await stockService.CheckAvailabilityAsync(request.Request.Products, cancellationToken);

        var order = orderFactory.Create(request.Request);

        await orderRepository.AddAsync(order, cancellationToken);

        logger.LogInformation("Order {OrderNumber} created successfully.", order.OrderNumber);
        return orderResponseMapper.Map(order, request.Request.Products);
    }
}