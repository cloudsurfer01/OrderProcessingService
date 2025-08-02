namespace Application.DTOs;

public class CreateOrderRequest
{
    public List<ProductItem> Products { get; set; } = new ();
    public string InvoiceAddress { get; set; } = string.Empty;
    public string DeliveryAddress { get; set; } = string.Empty;
    public string InvoiceCreditCardNumber { get; set; } = string.Empty;
    public string InvoiceEmailAddress { get; set; } = string.Empty;
}