using Domain.Entities;

namespace Application.Abstractions;

public interface IProfileService
{
    public Task<bool> DeleteAvatar(User user);
    public Task<bool> Rename(User user);
}