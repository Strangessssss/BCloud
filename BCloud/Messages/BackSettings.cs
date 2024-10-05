using System.IO;
using BCloud.Models.enums;

namespace BCloud.Messages;

public class BackSettings
{
    public BackSettings(string receivedFilesDirectory, int maxFilesCount, float maxFileSizeMb, ReceivingPermissionLevel permissionLevel)
    {
        ReceivedFilesDirectory = receivedFilesDirectory;
        MaxFilesCount = maxFilesCount;
        MaxFileSizeMb = maxFileSizeMb;
        PermissionLevel = permissionLevel;
    }

    public string ReceivedFilesDirectory { get; set; }
    public int MaxFilesCount { get; set; }
    public float MaxFileSizeMb { get; set; }
    public ReceivingPermissionLevel PermissionLevel { get; set; }
    
    
}