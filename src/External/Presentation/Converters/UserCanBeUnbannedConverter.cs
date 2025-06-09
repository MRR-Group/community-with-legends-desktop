using Domain.Entities;
using Domain.Enums;
using Infrastructure.Repositories;

namespace Presentation.Converters;

public class UserCanBeUnbannedConverter : UserConverter
{
    public static readonly UserCanBeUnbannedConverter Instance = new();

    protected override bool CanConvert(PermissionRepository permissions)
    {
        return permissions.Can(Permission.BanUsers);
    }

    protected override bool Convert(User user)
    {
        return user.IsBanned;
    }
}