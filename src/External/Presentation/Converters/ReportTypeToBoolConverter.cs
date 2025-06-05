using System;
using Avalonia.Data.Converters;
using Avalonia.Input;
using System.Globalization;
using Domain.Entities;

namespace Presentation.Converters;

public class ReportTypeToBoolConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is ResolvedReport;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}