using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.UseCases;

public class DeleteAdministratorInteractor
{
    private IAdministratorService _administratorService;

    public DeleteAdministratorInteractor(IAdministratorService administratorService)
    {
        _administratorService = administratorService;
    }

    public async Task<bool> DeleteAdministrator(Administrator administrator)
    {
        return await _administratorService.DeleteAdministrator(administrator);
    }
}