using Application.Abstractions;
using Application.Exceptions;
using Domain.Entities;

namespace Application.UseCases;

public class UnbanUserInteractor
{
    private IBanService _banService;

    public UnbanUserInteractor(IBanService banService)
    {
        _banService = banService;
    }

    public async Task<bool> Unban(User user)
    {
        if (!user.Permissions.HasNone())
        {
            throw new UserNotBannedException();
        }

        return await _banService.Unban(user);
    }
}