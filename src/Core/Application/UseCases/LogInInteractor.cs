using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.UseCases;

public class LogInInteractor
{
    private ILogInService _logInService;

    public LogInInteractor(ILogInService logInService)
    {
        _logInService = logInService;
    }

    public async Task<User> LogIn(Email email, Password password)
    {
        return await _logInService.LogIn(email, password);
    }
}