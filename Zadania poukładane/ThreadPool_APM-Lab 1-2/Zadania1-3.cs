using System;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace IOLab1
{
    class Program
    {
        //zadanie1
        static void ThreadProc(Object stateInfo)
        {
            int waitingTime = (int)((object[])stateInfo)[0];
            Thread.Sleep(waitingTime);
            Console.WriteLine(waitingTime + "ms elapsed");
        }

        static void ServerExample(Object stateInfo)
        {
            Int32 port = 1024;
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            TcpListener server = new TcpListener(ip, port);
            server.Start();


            byte[] buffer = new byte[256];
            String data = null;
            string helloMessage = "Hello client, nice to meet you";
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Client connected");

                NetworkStream stream = client.GetStream();

                int i;

                while ((i = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    data = System.Text.Encoding.ASCII.GetString(buffer, 0, i);
                    Console.WriteLine("Server received: {0}", data);


                    Console.WriteLine("Server sent: {0}", helloMessage);
                    byte[] message = System.Text.Encoding.ASCII.GetBytes(helloMessage.ToCharArray());
                    stream.Write(message, 0, message.Length);
                }
                client.Close();
            }

        }

        static void ServerExampleZadanie3(Object stateInfo)
        {
            Int32 port = 1024;
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            TcpListener server = new TcpListener(ip, port);
            server.Start();


            while (true)
            {                
                ThreadPool.QueueUserWorkItem(ClientHandler, server.AcceptTcpClient());
            }

        }


        static void ClientHandler(Object stateInfo)
        {
            TcpClient client = stateInfo as TcpClient;

            Console.WriteLine("Client connected");

            NetworkStream stream = client.GetStream();

            int i;

            byte[] buffer = new byte[256];
            String data = null;
            string helloMessage = "Hello client, nice to meet you";

            while ((i = stream.Read(buffer, 0, buffer.Length)) != 0)
            {
                data = System.Text.Encoding.ASCII.GetString(buffer, 0, i);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Server received: {0}", data);
                Console.ResetColor();

                //Console.WriteLine("Server sent: {0}", helloMessage);
                byte[] message = System.Text.Encoding.ASCII.GetBytes(helloMessage.ToCharArray());
                stream.Write(message, 0, message.Length);
            }
            client.Close();
        }

        static void ClientExample(Object stateInfo)
        {
            String servername = "localhost";
            Int32 port = 1024;
            String message = "Hello client " + stateInfo.ToString() + " here";


            TcpClient client = new TcpClient();
            client.Connect(servername, port);

            NetworkStream stream = client.GetStream();
            byte[] buffer = System.Text.Encoding.ASCII.GetBytes(message);

            //Console.WriteLine("Client{0} sent: {1}", stateInfo.ToString() ,message);
            stream.Write(buffer, 0, buffer.Length);

            buffer = new byte[256];


            int bytes = stream.Read(buffer, 0, buffer.Length);

            String responseData = System.Text.Encoding.ASCII.GetString(buffer);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Client {0} received: {1}",stateInfo.ToString(), responseData);
            Console.ResetColor();

            stream.Close();
            client.Close();
            //Console.WriteLine("Klient idzie spać");
            //Thread.Sleep(60000);

        }


        static void Main(string[] args)
        {

            ThreadPool.QueueUserWorkItem(ServerExampleZadanie3);

            ThreadPool.QueueUserWorkItem(ClientExample, 1);
            ThreadPool.QueueUserWorkItem(ClientExample, 0);
            ThreadPool.QueueUserWorkItem(ClientExample, 1);
            ThreadPool.QueueUserWorkItem(ClientExample, 2);
            ThreadPool.QueueUserWorkItem(ClientExample, 3);
            ThreadPool.QueueUserWorkItem(ClientExample, 4);
            ThreadPool.QueueUserWorkItem(ClientExample, 5);
            ThreadPool.QueueUserWorkItem(ClientExample, 6);
            ThreadPool.QueueUserWorkItem(ClientExample, 7);
            ThreadPool.QueueUserWorkItem(ClientExample, 8);




            ThreadPool.QueueUserWorkItem(ThreadProc, new object[] { 1000 });
            ThreadPool.QueueUserWorkItem(ThreadProc, new object[] { 500 });


            Thread.Sleep(60000);
        }
    }
}


/*Wnioski
 * zadanie2: Problem jest taki, że serwer może obsługiwać tylko jednego klienta w danym momencie
 * Kolory nie zawsze są prawidłowe, nie można przewidzieć w jakiej kolejności wykonają się wątki więc nie wiadomo jaki kolor jest w danym momencie ustawiony
 * 
 * /