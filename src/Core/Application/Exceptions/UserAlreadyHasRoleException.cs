using Domain.Enums;

namespace Application.Exceptions;

public class UserAlreadyHasRoleException : Exception
{
    public UserAlreadyHasRoleException(Role role) : base($"User already has the '{role.ToString().ToLower()}' role.")
    {
    }
}
