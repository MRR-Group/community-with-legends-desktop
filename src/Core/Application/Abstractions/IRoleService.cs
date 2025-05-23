using Domain.Entities;

namespace Application.Abstractions;

public interface IRoleService
{
    public Task<bool> GiveModeratorRole(User user);
    public Task<bool> RevokeModeratorRole(User user);
}