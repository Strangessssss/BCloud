namespace BCloud.Messages;

public class ReceivedFilesMessage
{
    public List<string>? Files { get; set; }
    public string? Destination { get; set; }
}