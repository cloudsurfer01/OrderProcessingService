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
    ILogger<CreateOrderCommandHandler> logger
        ) : IRequestHandler<CreateOrderCommand, OrderResponse>
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IProductStockChecker _productStockChecker = productStockChecker;
    private readonly ILogger<CreateOrderCommandHandler> _logger = logger;

    public async Task<OrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        foreach (var item in request.Request.Products)
        {
            if (!await productStockChecker.IsProductInStockAsync(item.ProductId, item.ProductAmount))
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

        await _orderRepository.AddAsync(order);

        _logger.LogInformation("Order {OrderNumber} created successfully.", order.OrderNumber);

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


