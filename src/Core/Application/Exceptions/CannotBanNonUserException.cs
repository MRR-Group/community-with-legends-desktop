namespace Application.Exceptions;

public class CannotBanNonUserException : Exception
{
    public CannotBanNonUserException() : base("Only users can be banned") { }
}