using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Repositories;

namespace Presentation.Converters;

public class CombinedRenameUserVisibilityConverter : CombinedVisibilityConverter<UserProfile>
{
    protected override bool HavePermission(PermissionRepository permissions, UserProfile target)
    {
        return permissions.Can(Permission.RenameUsers) && IsNotStaff(target.User);
    }
}
