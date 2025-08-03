namespace Domain.Entities;

public class ProductEntity
{
    public ProductEntity() { }

    public ProductEntity(Guid id, string name, decimal price, int availableQuantity)
    {
        Id = id;
        Name = name;
        Price = price;
        AvailableQuantity = availableQuantity;
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int AvailableQuantity { get; private set; }

    public bool IsInStock(int requestedQuantity) => AvailableQuantity >= requestedQuantity;

    public void ReduceStock(int quantity)
    {
        if (quantity > AvailableQuantity)
            throw new InvalidOperationException("Insufficient stock.");
        AvailableQuantity -= quantity;
    }
}



