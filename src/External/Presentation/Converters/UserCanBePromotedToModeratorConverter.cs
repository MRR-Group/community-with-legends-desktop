using Domain.Entities;
using Domain.Enums;
using Infrastructure.Repositories;

namespace Presentation.Converters;

public class UserCanBePromotedToModeratorConverter : UserConverter
{
    public static readonly UserCanBePromotedToModeratorConverter Instance = new();
    
    protected override bool CanConvert(PermissionRepository permissions)
    {
        return permissions.Can(Permission.ManageModerators);
    }

    protected override bool Convert(User user)
    {
        return user.Roles.IsNot(Role.Moderator, Role.Administrator, Role.SuperAdministrator);
    }
}