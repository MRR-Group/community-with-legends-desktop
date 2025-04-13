using Domain.Entities;

namespace Application.Abstractions;

using Domain.ValueObjects;

public interface ILogInService
{
    public Task<User> LogIn(Email email, Password password);
}