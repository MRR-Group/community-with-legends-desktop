using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Repositories;
namespace Presentation.Converters;

public abstract class UserConverter : IMultiValueConverter
{
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values is [User user, PermissionRepository permissions])
        {
            if (!CanConvert(permissions))
            {
                return false;
            }

            if (targetType.IsAssignableTo(typeof(bool)))
            {
                return Convert(user);
            }
        }

        return false;
    }
    
    protected virtual bool CanConvert(PermissionRepository permissions)
    {
        return true;
    }

    protected abstract bool Convert(User user);

    public object ConvertBack( object? value, Type targetType, object? parameter, CultureInfo culture )
    {
        throw new NotSupportedException();
    }
}