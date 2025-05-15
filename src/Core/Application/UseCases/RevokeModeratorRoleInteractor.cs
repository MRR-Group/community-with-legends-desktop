using Application.Abstractions;
using Application.Exceptions;
using Domain.Entities;
using Domain.Enums;

namespace Application.UseCases;

public class RevokeModeratorRoleInteractor
{
    private IRoleService _roleService;

    public RevokeModeratorRoleInteractor(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<bool> RevokeRole(User user)
    {
        if (!user.Roles.Is(Role.Moderator))
        {
            throw new UserDoesNotHaveRoleException(Role.Moderator);
        }

        return await _roleService.RevokeModeratorRole(user);
    }
}