using System.Text.RegularExpressions;

namespace Domain.ValueObjects;
public class Email
{
    public string Value { get; }

    public static readonly Regex EmailAddress = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Email cannot be empty.");
        if (!EmailAddress.IsMatch(value)) throw new ArgumentException("Invalid email format.");
        Value = value;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Email other) return false;
        return Value == other.Value;
    }

    public override int GetHashCode() => Value.GetHashCode();
}

