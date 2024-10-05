using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using BCloud.Services.Messages;
namespace BCloud.Services;

public class ServerManagerService
{
    private int _port = 8080;
    private TcpListener _listener;

    public ServerManagerService()
    {
        _listener = new TcpListener(IPAddress.Any, _port);
    }

    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        _listener.Start();
        Console.WriteLine($"Server started on port {_port} {_listener.LocalEndpoint.ToString()}.");

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var client = await _listener.AcceptTcpClientAsync();

                _ = HandleClientAsync(client);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            _listener.Stop();
        }
    }

    private async Task HandleClientAsync(TcpClient client)
    {
        using (client)
        {
            var stream = client.GetStream();
            var buffer = new byte[1024];
            int bytesRead;

            try
            {
                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    var json = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    var message = JsonSerializer.Deserialize<Message>(json);
                    Console.WriteLine($"Received from {message?.Sender}: {message?.Content}");

                    var responseJson = JsonSerializer.Serialize(new Message
                    {
                        Sender = "Server",
                        Content = $"Echo: {message?.Content}"
                    });
                    var responseBytes = Encoding.UTF8.GetBytes(responseJson);
                    await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Client connection error: {ex.Message}");
            }
        }

        Console.WriteLine("Client disconnected.");
    }
}