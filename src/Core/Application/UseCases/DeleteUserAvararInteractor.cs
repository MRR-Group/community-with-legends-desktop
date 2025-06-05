using Application.Abstractions;
using Domain.Entities;
using Domain.Primitives;

namespace Application.UseCases;

public class DeleteAvatarInteractor
{
    private IProfileService _profileService;

    public DeleteAvatarInteractor(IProfileService profileService)
    {
        _profileService = profileService;
    }

    public async Task<bool> DeleteAvatar(User user)
    {
        return await _profileService.DeleteAvatar(user);
    }
}