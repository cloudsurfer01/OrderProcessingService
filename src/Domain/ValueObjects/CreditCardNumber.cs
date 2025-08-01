namespace Domain.ValueObjects;

public class CreditCardNumber
{
    public string Value { get; }

    public CreditCardNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Credit card number cannot be empty.");
        if (!IsValidCreditCardNumber(value)) throw new ArgumentException("Invalid credit card number format.");
        Value = value;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not CreditCardNumber other) return false;
        return Value == other.Value;
    }

    public override int GetHashCode() => Value.GetHashCode();

    private static bool IsValidCreditCardNumber(string value)
    {
        var digitsOnly = new string(value.Where(char.IsDigit).ToArray());
        if (digitsOnly.Length < 13 || digitsOnly.Length > 19)
            return false; 

        // Simple Luhn algorithm check for credit card number validity
        var sum = 0;
        var alternate = false;
        for (var i = digitsOnly.Length - 1; i >= 0; i--)
        {
            var n = int.Parse(digitsOnly[i].ToString());
            if (alternate)
            {
                n *= 2;
                if (n > 9) n -= 9;
            }
            sum += n;
            alternate = !alternate;
        }
        return sum % 10 == 0;
    }
}

