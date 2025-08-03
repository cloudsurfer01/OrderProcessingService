namespace Application.DTOs;

public class OrderItemDto
{
    public string ProductId { get; set; } = null!;
    public string ProductName { get; set; } = null!;
    public int ProductAmount { get; set; }
    public decimal ProductPrice { get; set; }
}