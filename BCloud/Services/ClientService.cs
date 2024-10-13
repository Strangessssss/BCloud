using System.IO;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using BCloud.Services.Messages;
namespace BCloud.Services;

public class ClientService
{
    private readonly int _port;
    private readonly string _ip;
    private readonly string _name;
    
    
    public ClientService(string ip, int port)
    {
        _ip = ip;
        _port = port;
    }
    
    public async Task Connect()
    {
        await Task.Run(() =>
        {
            
        });
    }

    public void Register(SignUpMessage message)
    {
        var client = new TcpClient();
        client.ConnectAsync(IPAddress.Parse(_ip), _port);
        var metadata = new Metadata(message.Username)
        {
            MessageType = "Register",
            Password = message.Password
        };
        var metadataBytes = JsonSerializer.SerializeToUtf8Bytes(metadata);
        client.GetStream().Write(metadataBytes);
    }
    
    public void SendFiles(FileMessage fileMessage)
    {
        if (fileMessage.FilePaths == null) return;
        foreach (var mess in fileMessage.FilePaths)
        {
            _ = Task.Run(() =>
            {
                var client = new TcpClient();
                client.ConnectAsync(IPAddress.Parse(_ip), _port);
                using var stream = client.GetStream();
                using var fileStream = File.OpenRead(mess);
                var metadata = new Metadata(fileMessage.Sender)
                {
                    MessageType = "File",
                    FileSize = fileStream.Length,
                    FileName = Path.GetFileName(mess),
                    Destination = fileMessage.Destination
                };

                var metadataBytes = JsonSerializer.SerializeToUtf8Bytes(metadata);

                stream.Write(metadataBytes, 0, metadataBytes.Length);
                fileStream.CopyToAsync(stream);
            });
        }
    }
    
}