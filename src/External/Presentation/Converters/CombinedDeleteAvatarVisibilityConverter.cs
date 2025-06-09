using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Repositories;

namespace Presentation.Converters;

public class CombinedDeleteAvatarVisibilityConverter : CombinedVisibilityConverter<UserProfile>
{
    protected override bool HavePermission(PermissionRepository permissions, UserProfile target)
    {
        return permissions.Can(Permission.ChangeUsersAvatar) && IsNotStaff(target.User);
    }
}
