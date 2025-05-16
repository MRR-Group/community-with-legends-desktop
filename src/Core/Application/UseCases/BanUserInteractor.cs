using Application.Abstractions;
using Application.Exceptions;
using Domain.Entities;
using Domain.Enums;

namespace Application.UseCases;

public class BanUserInteractor
{
    private IBanService _banService;

    public BanUserInteractor(IBanService banService)
    {
        _banService = banService;
    }

    public async Task<bool> Ban(User user)
    {
        if (user.Permissions.Can(Permission.BanUsers))
        {
            throw new CannotBanNonUserException();
        }

        if (user.Permissions.HasNone())
        {
            throw new UserAlreadyBannedException();
        }

        return await _banService.Ban(user);
    }
}