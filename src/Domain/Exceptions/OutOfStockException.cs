namespace Domain.Exceptions;

public class OutOfStockException(string productName) : Exception($"Product '{productName}' is out of stock.")
{
}