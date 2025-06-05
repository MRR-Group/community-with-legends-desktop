using System;
using Avalonia.Data.Converters;
using Avalonia.Input;
using System.Globalization;

namespace Presentation.Converters;

public class BoolToHandCursorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is true)
        {
            return new Cursor(StandardCursorType.Hand);
        }

        return new Cursor(StandardCursorType.Arrow);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}