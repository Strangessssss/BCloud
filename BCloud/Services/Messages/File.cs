namespace BCloud.Services.Messages;

public class FileMessage(string filePath)
{
    public string FilePath { get; set; } = filePath;
}