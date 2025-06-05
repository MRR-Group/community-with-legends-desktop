using Domain.Entities;
using Domain.Enums;
using Infrastructure.Repositories;

namespace Presentation.Converters;

public class UserCanHaveAvatarChangedConverter : UserConverter
{
    public static readonly UserCanHaveAvatarChangedConverter Instance = new();
    
    protected override bool CanConvert(PermissionRepository permissions)
    {
        return permissions.Can(Permission.ChangeUsersAvatar);
    }

    protected override bool Convert(User user)
    {
        return !user.Permissions.HasNone() && user.Roles.IsNot(Role.Moderator, Role.Administrator, Role.SuperAdministrator);
    }
}