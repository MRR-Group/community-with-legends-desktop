using Application.Abstractions;
using Domain.Entities;
using Domain.Primitives;

namespace Application.UseCases;

public class DeleteUserHardwareInteractor
{
    private IHardwareService _hardwareService;

    public DeleteUserHardwareInteractor(IHardwareService hardwareService)
    {
        _hardwareService = hardwareService;
    }

    public async Task<bool> DeleteHardware(User user)
    {
        return await _hardwareService.DeleteHardware(user);
    }
}