using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatP2P
{
    public class Peer
    {
        private readonly TcpListener _listener;
        private TcpClient? _client;
        private const int Port = 8080;

        public Peer()
        {
            _listener = new TcpListener(IPAddress.Any, Port);
        }

        public async Task ConnectToPeer(string ipAddress, string port)
        {
            try
            {
                _client = new TcpClient(ipAddress, Convert.ToInt32(port));
                Console.WriteLine("Connected to peer");

                var receiveTask = ReceiveMessages();
                await SendMessages();
                await receiveTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection closed: " + ex.Message);
            }
            finally
            {
                Close();
            }
        }

        public async Task StartListening()
        {
            try
            {
                _listener.Start();
                Console.WriteLine("Listening for incoming connections...");
                _client = await _listener.AcceptTcpClientAsync();
                Console.WriteLine("Connected to peer");

                var receiveTask = ReceiveMessages();
                await SendMessages();
                await receiveTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection closed: " + ex.Message);
            }
            finally
            {
                Close();
            }
        }

        private async Task ReceiveMessages()
        {
            try
            {
                var stream = _client!.GetStream();
                var reader = new StreamReader(stream, Encoding.UTF8);

                while (_client.Connected)
                {
                    var message = await reader.ReadLineAsync();
                    if (message != null)
                    {
                        Console.WriteLine($"Peer message: {message}");
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error receiving message: {ex.Message}");
            }
        }

        private async Task SendMessages()
        {
            try
            {
                var stream = _client!.GetStream();
                var writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

                while (_client.Connected)
                {
                    var message = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(message))
                    {
                        await writer.WriteLineAsync(message);
                    }
                    else
                    {
                        break; 
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}");
            }
        }

        private void Close()
        {
            _client?.Close();
            _listener.Stop();
            Console.WriteLine("Connection closed");
        }
    }
}
