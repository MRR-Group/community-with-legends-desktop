using Application.Abstractions;
using Application.Exceptions;
using Domain.ValueObjects;

namespace Application.UseCases;

public class RegisterInteractor
{
    private IRegisterService _registerService;

    public RegisterInteractor(IRegisterService registerService)
    {
        _registerService = registerService;
    }

    public async Task Register(string name, Email email, Password password, Password repeatPassword)
    {
        if (password != repeatPassword)
        {
            throw new PasswordsDoNotMatchException();
        }

        await _registerService.Register(name, email, password);
    }
}