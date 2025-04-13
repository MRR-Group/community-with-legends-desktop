namespace Application.Abstractions;

using Domain.ValueObjects;

public interface IRegisterService
{
    public Task Register(string name, Email email, Password password);
}