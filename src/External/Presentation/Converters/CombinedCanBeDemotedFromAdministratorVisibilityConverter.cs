using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Repositories;

namespace Presentation.Converters;

public class CombinedCanBeDemotedFromAdministratorVisibilityConverter : CombinedVisibilityConverter<User>
{
    protected override bool HavePermission(PermissionRepository permissions, User target)
    {
        return permissions.Can(Permission.ManageAdministrators) && target.Roles.IsNot(Role.SuperAdministrator) && target.Roles.Is(Role.Administrator);
    }
}
