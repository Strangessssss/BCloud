using System.IO;

namespace BCloud.Services.Messages;

public class Message
{
    public Message(string sender)
    {
        Sender = sender;
    }

    public string Sender { get; set; }
    public List<string>? Content { get; set; }
    public string? Destination { get; set; }
}