using System;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace IOLab1
{
    class Program
    {
        private static readonly object SyncObject = new object();
        static void ServerExampleZadanie4(Object stateInfo)
        {
            Int32 port = 1024;
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            TcpListener server = new TcpListener(ip, port);
            server.Start();



            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                ThreadPool.QueueUserWorkItem(ClientConnectionHandler, client);
            }

        }


        static void ClientConnectionHandler(Object stateInfo)
        {
            TcpClient client = stateInfo as TcpClient;

            lock (SyncObject)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Client connected");
                Console.ResetColor();
            }



            NetworkStream stream = client.GetStream();

            int i;

            byte[] buffer = new byte[256];
            String data = null;
            string helloMessage = "Hello client, nice to meet you";

            while ((i = stream.Read(buffer, 0, buffer.Length)) != 0)
            {
                data = System.Text.Encoding.ASCII.GetString(buffer, 0, i);

                lock(SyncObject)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Server received: {0}", data);
                    Console.ResetColor();
                }



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

            lock(SyncObject)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Client {0} received: {1}", stateInfo.ToString(), responseData);
                Console.ResetColor();
            }


            stream.Close();
            client.Close();
            //Console.WriteLine("Klient idzie spać");
            //Thread.Sleep(60000);

        }


        static void Main(string[] args)
        {

            ThreadPool.QueueUserWorkItem(ServerExampleZadanie4);
            ThreadPool.QueueUserWorkItem(ClientExample, 0);
            ThreadPool.QueueUserWorkItem(ClientExample, 1);
            ThreadPool.QueueUserWorkItem(ClientExample, 2);
            ThreadPool.QueueUserWorkItem(ClientExample, 3);
            ThreadPool.QueueUserWorkItem(ClientExample, 4);
            ThreadPool.QueueUserWorkItem(ClientExample, 5);
            ThreadPool.QueueUserWorkItem(ClientExample, 6); 
            ThreadPool.QueueUserWorkItem(ClientExample, 7);
            ThreadPool.QueueUserWorkItem(ClientExample, 8);
            

            Thread.Sleep(60000);
        }
    }
}



/* Wnioski
 * Lock blokuje dostęp do obiektu, inne wątki nie mogą dostać się do obiektu dopóki dany wątek nie skończy pracy w sekcji lock{}
 * Wyswietlanie napisów działa poprawnie pod warunkiem, że są one w sekcji lock, jeżeli chcemy wypisać coś bez locka to napis dostanie losowy kolor.
 * 
 * Podsumowując lock rozwiązuje problem z kolorami, ale rezerwuje sobie obiekt, w tym przypadku konsolę, więc inne wątki nie mogą z tego korzystać
 */