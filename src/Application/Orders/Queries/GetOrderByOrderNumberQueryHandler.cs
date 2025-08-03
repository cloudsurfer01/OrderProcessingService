using Application.DTOs;
using Domain.Abstractions.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Orders.Queries;

public class GetOrderByOrderNumberQueryHandler(
                    IOrderRepository orderRepository,
                    ILogger<GetOrderByOrderNumberQueryHandler> logger): IRequestHandler<GetOrderByOrderNumberQuery, OrderResponse>
{
    public async Task<OrderResponse> Handle(GetOrderByOrderNumberQuery request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByOrderNumberAsync(request.OrderNumber, cancellationToken);

        if (order == null)
        {
            logger.LogWarning("Order not found: {OrderNumber}", request.OrderNumber);
            throw new KeyNotFoundException($"Order with order number {request.OrderNumber} not found.");
        }

        return new OrderResponse
        {
            OrderNumber = order.OrderNumber.ToString(),
            Products = order.Products.Select(p => new ProductItem
            {
                ProductId = p.ProductId,
                ProductAmount = p.ProductAmount,
                ProductName = p.ProductName,
                ProductPrice = p.ProductPrice
            }).ToList(),
            InvoiceAddress = order.InvoiceAddress?.Value ?? string.Empty,
            InvoiceEmailAddress = order.InvoiceEmailAddress?.Value ?? string.Empty,
            InvoiceCreditCardNumber = order.CreditCardNumber?.Value ?? string.Empty,
            CreatedAt = order.CreatedAt
        };
    }
}