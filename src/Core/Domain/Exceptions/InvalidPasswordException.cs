namespace Domain.Exceptions;

public class InvalidPasswordException : Exception
{
    public InvalidPasswordException() 
        : base("Password must be of 8 characters, contain lower and upper case letter") { }
}