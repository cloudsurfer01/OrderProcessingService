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
    ILogger<CreateOrderCommandHandler> logger
        ) : IRequestHandler<CreateOrderCommand, OrderResponse>
{
    public async Task<OrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        await stockService.CheckAvailabilityAsync(request.Request.Products, cancellationToken);

        var order = orderFactory.Create(request.Request);

        await orderRepository.AddAsync(order, cancellationToken);

        foreach (var item in request.Request.Products)
        {
            await productRepository.ReduceStockAsync(Guid.Parse(item.ProductId), item.ProductAmount, cancellationToken);
        }

        logger.LogInformation("Order {OrderNumber} created successfully.", order.OrderNumber);

        return new OrderResponse
        {
            OrderNumber = order.OrderNumber.ToString(),
            Products = request.Request.Products,
            InvoiceAddress = order.InvoiceAddress?.Value ?? string.Empty,
            InvoiceEmailAddress = order.InvoiceEmailAddress?.Value ?? string.Empty,
            InvoiceCreditCardNumber = order.CreditCardNumber?.Value ?? string.Empty,
            CreatedAt = order.CreatedAt
        };
    }
}