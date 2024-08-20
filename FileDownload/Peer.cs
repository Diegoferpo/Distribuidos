using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileDownload
{
    public class Peer {
        private readonly TcpListener _listener;
        private TcpClient _client;
        private const int Port = 8080;

        public Peer()
        {
            _listener = new TcpListener(IPAddress.Any, Port);
        }

        public async Task DownloadFile(string peerIP, int peerPort, string fileName, string savePath, CancellationToken cancellationToken)
        {
            _client = new TcpClient(peerIP, peerPort);
            await using var stream = _client.GetStream();
            var request = Encoding.UTF8.GetBytes(fileName);
            await stream.WriteAsync(request, cancellationToken);

            await using var fs = new FileStream(savePath, FileMode.Create, FileAccess.Write);
            var buffer = new byte[1024];
            int bytesRead;
            while ((bytesRead = await stream.ReadAsync(buffer, cancellationToken)) > 0)
            {
                await fs.WriteAsync(buffer.AsMemory(0, bytesRead), cancellationToken);
            }

            Console.WriteLine($"El archivo {fileName} se ha descargado en la ruta {savePath}");

            await ShowFileContent(savePath);
        }

        public async Task Start(CancellationToken cancellationToken)
        {
            _listener.Start();
            Console.WriteLine($"Servidor escuchando en el puerto {Port}...");

            while (true)
            {
                _client = await _listener.AcceptTcpClientAsync(cancellationToken);
                await HandleClient(cancellationToken);
            }
        }

        private async Task HandleClient(CancellationToken cancellationToken)
        {
            await using var stream = _client.GetStream();
            var buffer = new byte[1024];
            var bytesRead = await stream.ReadAsync(buffer, cancellationToken);
            var filename = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();

            if (File.Exists(filename))
            {
                var fileData = await File.ReadAllBytesAsync(filename, cancellationToken);
                await stream.WriteAsync(fileData, cancellationToken);
                Console.WriteLine($"El archivo {filename} se envió al cliente.");
            }
            else
            {
                var errorMessage = Encoding.UTF8.GetBytes("Archivo no encontrado");
                await stream.WriteAsync(errorMessage, cancellationToken);
                Console.WriteLine($"El archivo {filename} no se encontró.");
            }
        }

        private async Task ShowFileContent(string filePath)
        {
            try
            {
                var content = await File.ReadAllTextAsync(filePath);
                Console.WriteLine($"Contenido del archivo {filePath}: Diego F. Portillo Bibiano");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer el archivo {filePath}: {ex.Message}");
            }
        }

    }
}   
