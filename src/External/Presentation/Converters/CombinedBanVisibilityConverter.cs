using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Repositories;

namespace Presentation.Converters;

public class CombinedBanVisibilityConverter : CombinedVisibilityConverter<User>
{
    protected override bool HavePermission(PermissionRepository permissions, User target)
    {
        return !target.IsBanned && permissions.Can(Permission.BanUsers) && IsNotStaff(target);
    }
}
