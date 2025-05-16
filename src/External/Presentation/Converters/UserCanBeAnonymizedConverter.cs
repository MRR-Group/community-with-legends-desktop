using Domain.Entities;
using Domain.Enums;
using Infrastructure.Repositories;

namespace Presentation.Converters;

public class UserCanBeAnonymizedConverter : UserConverter
{
    public static readonly UserCanBeAnonymizedConverter Instance = new();
    
    protected override bool CanConvert(PermissionRepository permissions)
    {
        return permissions.Can(Permission.AnonymizeUsers);
    }

    protected override bool Convert(User user)
    {
        return user.Roles.IsNot(Role.Administrator, Role.Moderator, Role.SuperAdministrator);
    }
}