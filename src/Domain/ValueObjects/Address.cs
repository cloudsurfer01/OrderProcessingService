namespace Domain.ValueObjects;

public class Address
{
    public string Value { get; }

    public Address(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Address cannot be empty.");
        Value = value;
    }
    
    public override bool Equals(object? obj)
    {
        if (obj is not Address other) return false;
        return Value == other.Value;
    }

    public override int GetHashCode() => Value.GetHashCode();
}

