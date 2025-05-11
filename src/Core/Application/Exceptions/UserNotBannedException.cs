namespace Application.Exceptions;

public class UserNotBannedException : Exception
{
    public UserNotBannedException() : base("User is not banned") { }
}