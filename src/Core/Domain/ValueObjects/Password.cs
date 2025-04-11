using Domain.Exceptions;

namespace Domain.ValueObjects;

public sealed record Password
{
    public string Value { get; }

    public Password(string value)
    {
        if (!IsValid(value))
        {
            throw new InvalidPasswordException();
        }
        
        Value = value;
    }

    private bool IsValid(string value)
    {
        return value.Length >= 8 && value != value.ToLower() && value != value.ToUpper();
    }
    
    public static implicit operator string(Password password) => password.Value;
    
    public static implicit operator Password(string value) => new Password(value);
}