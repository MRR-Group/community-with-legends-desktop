using Domain.Entities;
using Domain.Enums;
using Infrastructure.Repositories;

namespace Presentation.Converters;

public class UserCanBeDemotedFromAdministratorConverter : UserConverter
{
    public static readonly UserCanBeDemotedFromAdministratorConverter Instance = new();
    
    protected override bool CanConvert(PermissionRepository permissions)
    {
        return permissions.Can(Permission.ManageAdministrators);
    }

    protected override bool Convert(User user)
    {
        return user.Roles.Is(Role.Administrator);
    }
}