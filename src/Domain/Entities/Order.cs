using Domain.ValueObjects;

namespace Domain.Entities;

public class Order
{
    public Guid OrderNumber { get; private set; }
    private readonly List<Product> _products = new();
    public IReadOnlyList<Product> Products => _products.AsReadOnly();
    public Address? InvoiceAddress { get; private set; }
    public Email? InvoiceEmailAddress { get; private set;  }
    public CreditCardNumber? CreditCardNumber { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Order() { }

    public Order(IEnumerable<Product> products, Address? invoiceAddress, Email? invoiceEmailAddress, CreditCardNumber? creditCardNumber)
    {
        if(!products.Any()) throw new ArgumentException("Order must contain at least one product.");

        OrderNumber = Guid.NewGuid();
        _products.AddRange(products);
        InvoiceAddress = invoiceAddress;
        InvoiceEmailAddress = invoiceEmailAddress;
        CreditCardNumber = creditCardNumber;
        CreatedAt = DateTime.UtcNow;
    }

    public decimal TotalPrice() => _products.Sum(p => p.TotalPrice());
}
