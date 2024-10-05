using System.Globalization;
using System.Windows.Data;
using BCloud.Models;

namespace BCloud.Converters;

public class UserStatusIdToImage : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var user = (UserProfile)value!;
        return user.StatusImageId is < 8 and > 0 ? $"../Appearance/Images/UserStatusImages/{user.StatusImageId}.png" : "../Appearance/Images/UserStatusImages/question.jpg";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }
}