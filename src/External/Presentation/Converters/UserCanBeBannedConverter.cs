using Domain.Entities;
using Domain.Enums;
using Infrastructure.Repositories;

namespace Presentation.Converters;

public class UserCanBeBannedConverter : UserConverter
{
    public static readonly UserCanBeBannedConverter Instance = new();
    
    protected override bool CanConvert(PermissionRepository permissions)
    {
        return permissions.Can(Permission.BanUsers);
    }

    protected override bool Convert(User user)
    {
        return !user.IsBanned && user.Roles.IsNot(Role.Moderator, Role.Administrator, Role.SuperAdministrator);
    }
}