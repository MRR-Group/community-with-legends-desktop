using Application.Abstractions;
using Application.Exceptions;
using Domain.ValueObjects;

namespace Application.UseCases;

public class SignUpInteractor
{
    private ISignInService SignInService;

    public SignUpInteractor(ISignInService signInService)
    {
        SignInService = signInService;
    }

    public async Task SignUp(string name, Email email, Password password, Password repeatPassword)
    {
        if (password != repeatPassword)
        {
            throw new PasswordsDoNotMatchException();
        }

        await SignInService.SignIn(name, email, password);
    }
}