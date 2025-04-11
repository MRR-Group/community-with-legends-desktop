namespace Domain.Exceptions;

public class InvalidEmailException : Exception
{
    public InvalidEmailException() 
        : base("Email must contain @ character") { }
}