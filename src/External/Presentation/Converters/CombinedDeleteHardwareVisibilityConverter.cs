using Domain.Entities;
using Domain.Enums;
using Infrastructure.Repositories;

namespace Presentation.Converters;

public class CombinedDeleteHardwareVisibilityConverter : CombinedVisibilityConverter<UserProfile>
{
    protected override bool HavePermission(PermissionRepository permissions, UserProfile target)
    {
        return target.HardwareList.Length > 0 && permissions.Can(Permission.DeleteUserHardware) && IsNotStaff(target.User);
    }
}
