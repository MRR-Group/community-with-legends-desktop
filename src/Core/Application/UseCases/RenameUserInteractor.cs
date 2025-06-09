using Application.Abstractions;
using Domain.Entities;
using Domain.Primitives;

namespace Application.UseCases;

public class RenameUserInteractor
{
    private IProfileService _profileService;

    public RenameUserInteractor(IProfileService profileService)
    {
        _profileService = profileService;
    }

    public async Task<bool> Rename(User user)
    {
        return await _profileService.Rename(user);
    }
}
