using Application.Abstractions;
using Application.Exceptions;
using Domain.Entities;
using Domain.Enums;

namespace Application.UseCases;

public class RevokeAdministratorRoleInteractor
{
    private IAdministratorService _administratorService;

    public RevokeAdministratorRoleInteractor(IAdministratorService administratorService)
    {
        _administratorService = administratorService;
    }
    
    public async Task<bool> RevokeRole(User administrator)
    {
        return await _administratorService.RevokeAdministratorRole(administrator);
    }
}