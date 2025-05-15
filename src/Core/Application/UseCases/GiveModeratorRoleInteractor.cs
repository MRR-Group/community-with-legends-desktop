using Application.Abstractions;
using Application.Exceptions;
using Domain.Entities;
using Domain.Enums;

namespace Application.UseCases;

public class GiveModeratorRoleInteractor
{
    private IRoleService _roleService;

    public GiveModeratorRoleInteractor(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<bool> GiveRole(User user)
    {
        if (user.Roles.Is(Role.Moderator))
        {
            throw new UserAlreadyHasRoleException(Role.Moderator);
        }

        return await _roleService.GiveModeratorRole(user);
    }
}