using Domain.Entities;
using Domain.Enums;
using Infrastructure.Repositories;

namespace Presentation.Converters;

public class UserCanBeDemotedFromModeratorConverter : UserConverter
{
    public static readonly UserCanBeDemotedFromModeratorConverter Instance = new();
    
    protected override bool CanConvert(PermissionRepository permissions)
    {
        return permissions.Can(Permission.ManageModerators);
    }

    protected override bool Convert(User user)
    {
        return user.Roles.Is(Role.Moderator);
    }
}