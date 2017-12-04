using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InzynieriaOprogramowaniaLab4
{
    class Program
    {
        static async Task serverTask()
        {
            TcpListener server = new TcpListener(IPAddress.Any, 2048);
            server.Start();
            while (true)
            {
                TcpClient client = await server.AcceptTcpClientAsync();
                byte[] buffer = new byte[1024];
                client.GetStream().ReadAsync(buffer, 0, buffer.Length).ContinueWith(
                    async (t) =>
                    {
                        int i = t.Result;
                        while (true)
                        {
                            client.GetStream().WriteAsync(buffer, 0, i);
                            i = await client.GetStream().ReadAsync(buffer, 0, buffer.Length);
                        }
                    });
            }
        }

        static void Main(string[] args)
        {
            Server server = new Server();
            server.Run();


            Client client = new Client();
            Client client2 = new Client();
            client.Connect();
            client2.Connect();

            CancellationTokenSource token1 = new CancellationTokenSource();
            CancellationTokenSource token2 = new CancellationTokenSource();

            var c1=client.keepPinging("heehe", token1.Token);
            var c2 = client2.keepPinging("huhuh", token2.Token);


            token1.CancelAfter(2000);
            token2.CancelAfter(3000);

            Task.WaitAll(new Task[] {c1,c2 });

            try
            {
                server.StopRunning();
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
