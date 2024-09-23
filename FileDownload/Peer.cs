using System.Net;
using System.Net.Sockets;
using System.Text;

namespace FileDownload;

public class Peer
{
    private readonly TcpListener _listener; // Escucha conexiones entrantes.
    private TcpClient _client; // Cliente TCP usado para conectarse a otros peers.
    private const int Port = 8080; // Puerto predeterminado para las conexiones.

    public Peer() {
        _listener = new TcpListener(IPAddress.Any, Port); // Inicializa el listener para escuchar en cualquier IP.
    }

    // Método para descargar un archivo desde otro peer.
    public async Task DownloadFile(string peerIP, int peerPort, string fileName, string savePath, CancellationToken cancellationToken){
        _client = new TcpClient(peerIP, peerPort); // Se conecta al peer servidor especificando IP y puerto.
        await using var stream = _client.GetStream(); // Obtiene el stream de datos para la conexión.
        
        var request = Encoding.UTF8.GetBytes(fileName); // Convierte el nombre del archivo a bytes.
        await stream.WriteAsync(request, 0, request.Length, cancellationToken); // Envía el nombre del archivo al peer servidor

        await using var fs = new FileStream(savePath, FileMode.Create, FileAccess.Write); // Crea un archivo local para almacenar los datos recibidos.
        var buffer = new byte[1024]; // Buffer para almacenar los datos recibidos.
        int bytesRead;
        // Lee los datos desde el stream y los escribe en el archivo local.
        while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0){
            await fs.WriteAsync(buffer, 0, bytesRead, cancellationToken);
        }
        Console.WriteLine($"El archivo {fileName} se ha descargado en la ruta {savePath}");
    }

    // Método para iniciar el peer servidor.
    public async Task Start(CancellationToken cancellationToken){
        _listener.Start();
        Console.WriteLine("Listening for incoming connections...");
        
      
        while (!cancellationToken.IsCancellationRequested){
            _client = await _listener.AcceptTcpClientAsync(cancellationToken); 
            await HandleClient(cancellationToken); 
        }
    }
    private async Task HandleClient(CancellationToken cancellationToken){
        await using var stream = _client.GetStream(); 
        
        var buffer = new byte[1024]; 
        var bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken); 
        var fileName = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim(); 

        if (File.Exists(fileName)){
            var fileData = await File.ReadAllBytesAsync(fileName, cancellationToken); 
            await stream.WriteAsync(fileData, 0, fileData.Length, cancellationToken); 
            Console.WriteLine($"File {fileName} sent to client.");
        } else {
            var errorMessage = Encoding.UTF8.GetBytes("File not found."); 
            await stream.WriteAsync(errorMessage, 0, errorMessage.Length, cancellationToken); 
            Console.WriteLine($"File {fileName} not found.");
        }
    }
}
