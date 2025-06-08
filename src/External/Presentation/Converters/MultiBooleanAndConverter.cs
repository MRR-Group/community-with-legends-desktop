using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Presentation.Converters;

public class MultiBooleanAndConverter : IMultiValueConverter
{
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        foreach (var value in values)
        {
            if (value is bool b)
            {
                if (!b)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        return true;
    }
}