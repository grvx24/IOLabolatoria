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
    class Server
    {
        TcpListener server;
        int port;
        IPAddress address;
        bool running = false;
        CancellationTokenSource cts = new CancellationTokenSource();

        Task serverTask;
        public Task ServerTask
        {
            get { return serverTask; }
        }

        public IPAddress Address
        {
            get { return address; }
            set
            {
                if (!running) address = value;
                else;
            }
        }
        public int Port
        {
            get { return port; }
            set
            {
                if (!running)
                    port = value;
                else;
            }
        }

        public Server()
        {
            Address = IPAddress.Any;
            port = 2048;
        }
        public Server(int port)
        {
            this.port = port;
        }
        public Server(IPAddress address)
        {
            this.address = address;
        }

        public async Task RunAsync(CancellationToken ct)
        {
            server = new TcpListener(address, port);

            try
            {
                server.Start();
                running = true;
            }
            catch (SocketException ex)
            {
                throw (ex);
            }
            while (true && !ct.IsCancellationRequested)
            {
                TcpClient client = await server.AcceptTcpClientAsync();
                byte[] buffer = new byte[64];
                using (ct.Register(() => client.GetStream().Close()))
                {
                    await client.GetStream().ReadAsync(buffer, 0, buffer.Length).ContinueWith(
                    async (t) =>
                    {
                        int i = t.Result;
                        while (true)
                        {
                            client.GetStream().WriteAsync(buffer, 0, i);
                            try
                            {
                                i = await client.GetStream().ReadAsync(buffer, 0, buffer.Length);
                                Console.WriteLine(Encoding.ASCII.GetString(buffer));
                            }
                            catch
                            {
                                break;
                            }
                            
                        }
                    });
                }

            }

        }
        public void RequestCancellation()
        {
            cts.Cancel();
            //serverTask.Wait();
            //serverTask.Dispose();
            server.Stop();
        }
        public void Run()
        {

            serverTask = RunAsync(cts.Token);
        }
        public void StopRunning()
        {
            RequestCancellation();
            //serverTask.Dispose();
        }

    }
}
