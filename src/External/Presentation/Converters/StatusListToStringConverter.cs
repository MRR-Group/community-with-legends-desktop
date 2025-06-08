using System;
using Avalonia.Data.Converters;
using Avalonia.Input;
using System.Globalization;
using System.Linq;
using Domain.Enums;

namespace Presentation.Converters;

public class StatusListToStringConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is ReportStatus[] status)
        {
            return string.Join(", ", status);
        }

        return "";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}