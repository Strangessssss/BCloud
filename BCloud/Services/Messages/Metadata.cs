using BCloud.Models;

namespace BCloud.Services.Messages;

public class Metadata(string sender, string type = "File")
{
    public string? Sender { get; set; } = sender;
    public long FileSize { get; set; }
    public string? FileName { get; set; }
    public string? Destination { get; set; }
    public string? MessageType { get; set; } = type;
    public string? Password { get; set; }
}