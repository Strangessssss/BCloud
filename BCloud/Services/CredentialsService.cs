using System.IO;
using System.Text.Json;

namespace BCloud.Services;

public static class CredentialsService
{
    public static string? Name
    {
        get
        {
            var credentials = GetCredentials();
            return credentials?["Username"];
        }
        set
        {
            var credentials = GetCredentials();
            if (credentials != null) 
                credentials["Username"] = value!;
        }
    }
    
    public static string? Password
    {
        get
        {
            var credentials = GetCredentials();
            return credentials?["PasswordHash"];
        }
        set
        {
            var credentials = GetCredentials();
            if (credentials != null) 
                credentials["PasswordHash"] = value!;
        }
    }

    private static Dictionary<string, string>? GetCredentials()
    {
        var readCredentials = File.ReadAllText("credentials.json");
        var credentials = JsonSerializer.Deserialize<Dictionary<string, string>>(readCredentials);
        return credentials ?? null;
    }
}