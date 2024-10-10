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
    
    
    public ClientService(string ip, int port, string name)
    {
        _ip = ip;
        _port = port;
        _name = name;
    }
    
    public async Task Connect()
    {
        await Task.Run(() =>
        {
            var message = new Message(_name);
            Send(message);
        });
    }
    
    public void Send(Message message)
    {
        if (message.Content == null) return;
        foreach (var mess in message.Content)
        {
            _ = Task.Run(() =>
            {
                var client = new TcpClient();
                client.ConnectAsync(IPAddress.Parse(_ip), _port);
                using var stream = client.GetStream();
                using var fileStream = File.OpenRead(mess);
                var metadata = new Metadata(message.Sender)
                {
                    FileSize = fileStream.Length,
                    FileName = Path.GetFileName(mess),
                    Destination = message.Destination
                };

                var metadataBytes = JsonSerializer.SerializeToUtf8Bytes(metadata);

                stream.Write(metadataBytes, 0, metadataBytes.Length);
                fileStream.CopyToAsync(stream);
            });
        }
    }
    
}