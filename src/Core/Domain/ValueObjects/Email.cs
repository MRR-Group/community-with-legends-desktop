using Domain.Exceptions;

namespace Domain.ValueObjects;

public sealed record Email
{
    public string Value { get; }

    public Email(string value)
    {
        if (!IsValid(value))
        {
            throw new InvalidEmailException();
        }
        
        Value = value;
    }

    private bool IsValid(string value)
    {
        return value.Contains('@');
    }
    
    public static implicit operator string(Email email) => email.Value;
    
    public static implicit operator Email(string value) => new Email(value);
}