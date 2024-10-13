using System.IO;

namespace BCloud.Services.Messages;

public class FileMessage(string sender)
{
    public string Sender { get; set; } = sender;
    public List<string>? FilePaths { get; set; }
    public string? Destination { get; set; }
}