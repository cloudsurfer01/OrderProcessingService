namespace Domain.Entities;

public class Product(Guid id, string name, decimal price, int availableQuantity)
{
    public Guid Id { get; private set; } = id;
    public string Name { get; private set; } = name;
    public decimal Price { get; private set; } = price;
    public int AvailableQuantity { get; private set; } = availableQuantity;

    public bool IsInStock(int requestedQuantity) => AvailableQuantity >= requestedQuantity;

    public void ReduceStock(int quantity)
    {
        if (quantity > AvailableQuantity)
            throw new InvalidOperationException("Insufficient stock.");
        AvailableQuantity -= quantity;
    }
}


