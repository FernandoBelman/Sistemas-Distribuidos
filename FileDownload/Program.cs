namespace FileDownload;

public class Program
{
    public static async Task Main(string[] args)
    {
        var cancellationTokenSource = new CancellationTokenSource();
        var peer = new Peer();
        var task = peer.Start(cancellationTokenSource.Token);

        if (args.Length > 0 && args[0] == "download")
        {
            if (args.Length < 5)
            {
                Console.WriteLine("Uso incorrecto. Por favor, proporcione los argumentos: download <peerIp> <peerPort> <fileName> <savePath>");
                return;
            }
            await peer.DownloadFile(args[1], Convert.ToInt32(args[2]), args[3], args[4], cancellationTokenSource.Token);
        }
        else
        {
            Console.WriteLine("Esperando a que otros peers se conecten...");
        }

        await task;
    }
}
