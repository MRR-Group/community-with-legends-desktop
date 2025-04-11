namespace Application.Abstractions;

using Domain.ValueObjects;

public interface ISignInService
{
    public Task SignIn(string name, Email email, Password password);
}