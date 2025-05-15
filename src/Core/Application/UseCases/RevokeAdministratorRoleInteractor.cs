using Application.Abstractions;
using Application.Exceptions;
using Domain.Entities;
using Domain.Enums;

namespace Application.UseCases;

public class RevokeAdministratorRoleInteractor
{
    private IRoleService _roleService;

    public RevokeAdministratorRoleInteractor(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<bool> RevokeRole(User user)
    {
        if (!user.Roles.Is(Role.Administrator))
        {
            throw new UserDoesNotHaveRoleException(Role.Administrator);
        }

        return await _roleService.RevokeAdministratorRole(user);
    }
}