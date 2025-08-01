namespace Application.DTOs;

public class CreateOrderRequest
{
    public List<ProductItem> Products { get; set; } = new ();
    public string InvoiceAddress { get; set; } = string.Empty;
    public string DeliveryAddress { get; set; } = string.Empty;
    public string InvoiceCreditCardNumber { get; set; } = string.Empty;
}

public class ProductItem
{
    public string ProductId { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public int ProductAmount { get; set; }
    public decimal ProductPrice { get; set; } = 0.0m;
}


