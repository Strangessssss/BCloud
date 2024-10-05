using BCloud.Models;

namespace BCloud.Converters;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

public class FirstItemBackgroundConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (((UserProfile)value!).Authorized)
        {
            return "#ffbf00";
        }
        return Brushes.Transparent;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
