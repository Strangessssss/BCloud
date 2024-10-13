namespace BCloudServer.Services;

public class Metadata(string sender)
{
    public string? Sender { get; set; } = sender;
    public long FileSize { get; set; }
    public string? FileName { get; set; }
    public string? Destination { get; set; }
    public string? MessageType { get; set; }
    public string? Password { get; set; }
}