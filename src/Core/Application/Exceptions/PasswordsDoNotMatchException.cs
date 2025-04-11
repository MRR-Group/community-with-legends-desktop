namespace Application.Exceptions;

public class PasswordsDoNotMatchException : Exception
{
    public PasswordsDoNotMatchException() : base("Passwords don't match") { }
}