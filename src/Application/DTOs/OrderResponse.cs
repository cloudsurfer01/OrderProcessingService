﻿namespace Application.DTOs;

public class OrderResponse
{
    public string OrderNumber { get; set; } = string.Empty;
    public List<ProductItem> Products { get; set; } = new();
    public string InvoiceAddress { get; set; } = string.Empty;
    public string InvoiceCreditCardNumber { get; set; } = string.Empty;
    public string InvoiceEmailAddress { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}