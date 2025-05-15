using Domain.Enums;

namespace Application.Exceptions;

public class UserDoesNotHaveRoleException : Exception
{
    public UserDoesNotHaveRoleException(Role role) : base($"User does not have the '{role.ToString().ToLower()}' role.")
    {
    }
}
