namespace Domain.Exceptions;

public class InvalidPasswordException : Exception
{
    public InvalidPasswordException() 
        : base("Password must consist of at least 8 letters and contain one uppercase and one lowercase character.") { }
}