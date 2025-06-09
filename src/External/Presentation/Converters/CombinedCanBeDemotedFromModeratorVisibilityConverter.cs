using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Repositories;

namespace Presentation.Converters;

public class CombinedCanBeDemotedFromModeratorVisibilityConverter : CombinedVisibilityConverter<User>
{
    protected override bool HavePermission(PermissionRepository permissions, User target)
    {
        return permissions.Can(Permission.ManageModerators) && target.Roles.IsNot(Role.SuperAdministrator, Role.Administrator) && target.Roles.Is(Role.Moderator);
    }
}