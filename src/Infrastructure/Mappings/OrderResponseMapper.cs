using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Mappings;

public class OrderResponseMapper : IOrderResponseMapper
{
    public OrderResponse Map(Order order, IEnumerable<ProductItem> products)
    {
        return new OrderResponse
        {
            OrderNumber = order.OrderNumber.ToString(),
            Products = products.ToList(),
            InvoiceAddress = order.InvoiceAddress?.Value ?? string.Empty,
            InvoiceEmailAddress = order.InvoiceEmailAddress?.Value ?? string.Empty,
            InvoiceCreditCardNumber = order.CreditCardNumber?.Value ?? string.Empty,
            CreatedAt = order.CreatedAt
        };
    }
}