namespace Domain.ValueObjects;

public class Product
{
    public string ProductId { get; }
    public string ProductName { get; }
    public int ProductAmount { get; }
    public decimal ProductPrice { get; }

    public Product(string productId, string productName, int productAmount, decimal productPrice)
    {
        if (string.IsNullOrWhiteSpace(productId)) throw new ArgumentException("Product ID is required.");
        if (string.IsNullOrWhiteSpace(productName)) throw new ArgumentException("Product name is required.");
        if (productAmount <= 0) throw new ArgumentException("Product amount must be greater than zero.");
        if (productPrice < 0) throw new ArgumentException("Product price cannot be negative.");
        ProductId = productId;
        ProductName = productName;
        ProductAmount = productAmount;
        ProductPrice = productPrice;
    }

    public decimal TotalPrice() => ProductAmount * ProductPrice;

    public override bool Equals(object? obj)
    {
        if (obj is not Product other) return false;
        return ProductId == other.ProductId &&
               ProductName == other.ProductName &&
               ProductAmount == other.ProductAmount &&
               ProductPrice == other.ProductPrice;
    }

    public override int GetHashCode() => HashCode.Combine(ProductId, ProductName, ProductAmount, ProductPrice);
    
}



