using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace BCloudServer.Services;

public class ServerManager
{
    
    private readonly int _port;
    private readonly IPAddress _ip;
    private readonly CancellationToken _cancellationToken = new();
    private readonly TcpListener _listener;

    public ServerManager(string ip, int port)
    {
        _ip = IPAddress.Parse(ip);
        _port = port;
        _listener = new TcpListener(_ip, _port);
    }
    
    public async Task StartServer()
    {
        _listener.Start();
        while (!_cancellationToken.IsCancellationRequested)
        {
            var client = await _listener.AcceptTcpClientAsync(_cancellationToken);
            _ = Task.Run(() => HandleClient(client), _cancellationToken);
        }
    }

    private async Task HandleClient(TcpClient client)
    {
        await using var stream = client.GetStream();
        
        var metadataBuffer = new byte[1024]; 
        var bytesRead = await stream.ReadAsync(metadataBuffer, 0, metadataBuffer.Length, _cancellationToken);
        var metadataJson = Encoding.UTF8.GetString(metadataBuffer, 0, bytesRead);
        var metadata = JsonSerializer.Deserialize<Metadata>(metadataJson);

        switch (metadata?.MessageType)
        {
            case "File":
                await AcceptFile(metadata, stream);
                break;
            case "Register":
                
                break;
        }
    }

    private async Task AcceptFile(Metadata metadata, Stream stream)
    {
        var fileStream = File.Create(metadata!.FileName!);
        var fileBuffer = new byte[1024];
        var bytesRemaining = metadata.FileSize;
        
        while (bytesRemaining > 0)
        {
            var read = await stream.ReadAsync(fileBuffer, 0, (int)Math.Min(fileBuffer.Length, bytesRemaining), _cancellationToken);
            if (read == 0) break;
            await fileStream.WriteAsync(fileBuffer, 0, read, _cancellationToken);
            bytesRemaining -= read;
        }
        fileStream.Close();
    }

    private async Task Register(Metadata metadata)
    {
        
    }
}