using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Repositories;

namespace Presentation.Converters;

public abstract class CombinedVisibilityConverter<T> : IMultiValueConverter
{
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Count != 3)
            return false;
        
        if (values[0] is not bool isResolved ||
            values[1] is not T t||
            values[2] is not PermissionRepository permissions)
        {
            return false;
        }

        if (isResolved)
        {
            return false;
        }

        return HavePermission(permissions, t);
    }

    protected abstract bool HavePermission(PermissionRepository permissions, T target);
    
    protected bool IsNotStaff(User user)
    {
        return user.Roles.IsNot(Role.Moderator, Role.Administrator, Role.SuperAdministrator);
    }
    
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
