using System;
using Avalonia.Data.Converters;
using Avalonia.Input;
using System.Globalization;
using Domain.Entities;

namespace Presentation.Converters;

public class ReportToResolvedDateConverted : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is ResolvedReport report)
        {
            return report.ResolvedAt;
        }

        return null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}