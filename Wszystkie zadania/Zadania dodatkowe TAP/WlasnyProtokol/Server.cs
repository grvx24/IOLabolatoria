using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WlasnyProtokol
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
                if (!running)
                    address = value;
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

        public Server(IPAddress address,int port)
        {
            this.address = address;
            this.port = port;
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
                throw(ex);
            }

            while(true && !ct.IsCancellationRequested)
            {
                TcpClient client = await server.AcceptTcpClientAsync();
                byte[] buffer = new byte[64];

                using (ct.Register(() => client.GetStream().Close()))
                {

                    await client.GetStream().ReadAsync(buffer, 0, buffer.Length, ct).ContinueWith(
                        async (t) =>
                        {
                            int i = t.Result;
                            while(true)
                            {
                                Thread.Sleep(1000);

                                try
                                {
                                    var messageFromClient = Encoding.ASCII.GetString(buffer);
                                    Console.WriteLine("Serwer otrzymuje " + messageFromClient);
                                    if(messageFromClient.StartsWith("HI"))
                                    {
                                        Console.WriteLine("Serwer odpowiada: ACK");
                                        var msg = Encoding.ASCII.GetBytes("ACK");
                                        await client.GetStream().WriteAsync(msg, 0, msg.Length, ct);
                                    }
                                    if (messageFromClient.StartsWith("TAK"))
                                    {
                                        Console.WriteLine("Serwer odpowiada: ACK-TAK");
                                        var msg = Encoding.ASCII.GetBytes("ACK-TAK");
                                        await client.GetStream().WriteAsync(msg, 0, msg.Length, ct);
                                    }
                                    if (messageFromClient.StartsWith("NIE"))
                                    {
                                        Console.WriteLine("Serwer odpowiada: ACK-NIE");
                                        var msg = Encoding.ASCII.GetBytes("ACK-NIE");
                                        await client.GetStream().WriteAsync(msg, 0, msg.Length, ct);
                                    }

                                    try
                                    {
                                        i = await client.GetStream().ReadAsync(buffer, 0, buffer.Length);
                                    }
                                    catch
                                    {
                                        break;
                                    }

                                }
                                catch (Exception)
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
            server.Stop();
        }

        public void Run()
        {
            serverTask = RunAsync(cts.Token);
            Console.WriteLine("Serwer uruchomiony");
        }

    }
}
