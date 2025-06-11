using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.UseCases;

public class CreateAdministratorUserInteractor
{
    private IAdministratorService _administratorService;

    public CreateAdministratorUserInteractor(IAdministratorService administratorService)
    {
        _administratorService = administratorService;
    }

    public async Task<Administrator> CreateAdministrator(string name, Email email, Password password)
    { 
        return await _administratorService.CreateAdministrator(name, email, password);
    }
}