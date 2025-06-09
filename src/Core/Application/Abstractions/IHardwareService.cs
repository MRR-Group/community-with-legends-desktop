using Domain.Entities;

namespace Application.Abstractions;

public interface IHardwareService
{
    public Task<bool> DeleteHardware(User user);
}