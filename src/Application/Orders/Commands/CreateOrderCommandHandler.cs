using Application.DTOs;
using Domain.Abstractions.Repositories;
using MediatR;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Application.Orders.Commands;

public class CreateOrderCommandHandler(
    IOrderRepository orderRepository,
    IProductStockChecker productStockChecker,
    IProductRepository productRepository,
    ILogger<CreateOrderCommandHandler> logger
        ) : IRequestHandler<CreateOrderCommand, OrderResponse>
{
    public async Task<OrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        foreach (var item in request.Request.Products)
        {
            if (!await productStockChecker.IsProductInStockAsync(item.ProductId, item.ProductAmount, cancellationToken))
            {
                logger.LogWarning("Product {ProductId} is out of stock.", item.ProductId);
                throw new InvalidOperationException($"Product {item.ProductId} is out of stock.");
            }
        }

        var order = new Order(
            request.Request.Products.Select(p =>
                new Product(p.ProductId, p.ProductName, p.ProductAmount, p.ProductPrice)),
            new Address(request.Request.InvoiceAddress),
            new Email(request.Request.InvoiceEmailAddress),
            new CreditCardNumber(request.Request.InvoiceCreditCardNumber)
        );

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


