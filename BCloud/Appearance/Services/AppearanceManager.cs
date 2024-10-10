using System.IO;
using System.Text.Json;

namespace BCloud.Appearance.Services;

public static class AppearanceManager
{
    public static Dictionary<string, string>? ConvertJsonToDictionary(string json)
    {
        return JsonSerializer.Deserialize<Dictionary<string, string>>(json);
    }
    
    public static Dictionary<string, string>? GetThemeInfo(string settingsFileName)
    {
        var json = File.ReadAllText($"Appearance/Themes/{GetTheme(settingsFileName)}.json");
        var colors = ConvertJsonToDictionary(json);
        return colors;
    }

    public static string GetTheme(string settingsFileName)
    {
        return GetSettings(settingsFileName)!["Theme"];
    }
    
    public static string GetThemeImage(string settingsFileName)
    {
        var info = GetThemeInfo(settingsFileName);
        return info!["ThemeImage"];
    }

    public static Dictionary<string, string>? GetSettings(string settingsFileName)
    {
        var json = File.ReadAllText(settingsFileName);
        var settings = ConvertJsonToDictionary(json); 
        return settings;
    }
    
    public static bool IsAnimationOn(string settingsFileName)
    {
        if (GetSettings(settingsFileName)?["Animation"] == "On")
        {
            return true;
        }
        return false;
    }
}