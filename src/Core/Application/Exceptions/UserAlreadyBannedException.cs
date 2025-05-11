namespace Application.Exceptions;

public class UserAlreadyBannedException : Exception
{
    public UserAlreadyBannedException() : base("User is already banned") { }
}