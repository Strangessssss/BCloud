namespace BCloudServer.Services.Entities;

public class User
{
    public User(string id, string name, string password, string receivingPermission, long maxFileSize, long maxFilesCount)
    {
        Id = id;
        Name = name;
        Password = password;
        ReceivingPermission = receivingPermission;
        MaxFileSize = maxFileSize;
        MaxFilesCount = maxFilesCount;
    }

    public string Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    
    public string ReceivingPermission { get; set; }
    public long MaxFileSize { get; set; }
    public long MaxFilesCount { get; set; }
}