using System.Globalization;
using System.Windows.Data;
using BCloud.Models;

namespace BCloud.Converters;

public class UserStatusIdToImage : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var user = (UserProfile)value!;
        return $"../Appearance/Images/UserStatusImages/{user.StatusImageId}.png";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }
}