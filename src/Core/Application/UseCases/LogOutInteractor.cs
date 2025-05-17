using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.UseCases;

public class LogOutInteractor
{
    private ILogOutService _logOutService;

    public LogOutInteractor(ILogOutService logOutService)
    {
        _logOutService = logOutService;
    }

    public async Task LogOut()
    { 
        await _logOutService.LogOut();
    }
}