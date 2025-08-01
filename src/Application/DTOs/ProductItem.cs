namespace Application.DTOs;

public class ProductItem
{
    public string ProductId { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public int ProductAmount { get; set; }
    public decimal ProductPrice { get; set; } = 0.0m;
}