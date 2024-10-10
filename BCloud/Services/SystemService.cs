using System.Management;

namespace BCloud.Services;

public static class SystemService
{
    public static string? GetMacAdresses()
    {
        var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapter WHERE PhysicalAdapter=True");
        foreach (var item in searcher.Get())
        {
            return item["MacAddress"].ToString();
        }
        return null;
    }
}