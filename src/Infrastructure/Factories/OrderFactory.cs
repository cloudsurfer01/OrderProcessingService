using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;

namespace Infrastructure.Factories;

public class OrderFactory : IOrderFactory
{
    public Order Create(CreateOrderRequest dto)
    {
        var products = dto.Products.Select(p =>
            new Product(p.ProductId, p.ProductName, p.ProductAmount, p.ProductPrice)).ToList();

        return new Order(
            products,
            new Address(dto.InvoiceAddress),
            new Email(dto.InvoiceEmailAddress),
            new CreditCardNumber(dto.InvoiceCreditCardNumber)
        );
    }
}