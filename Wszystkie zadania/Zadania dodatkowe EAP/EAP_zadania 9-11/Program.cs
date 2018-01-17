using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EAP_zadania
{
    class Program
    {
        static void RunBackgroundWorkerTcpListener()
        {
            BackgroundWorkerTcpListener listener = new BackgroundWorkerTcpListener();
            TcpClient client1 = new TcpClient();
            client1.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1025));
            byte[] message = new ASCIIEncoding().GetBytes("Klient1 wysyla wiadomosc");
            client1.GetStream().Write(message, 0, message.Length);
            client1.GetStream().Read(message, 0, message.Length);
            client1.Close();

            listener.CancelWorker();

        }

        static void Main(string[] args)
        {

            //foo = new EventHandler(MatrixMultiplication);
            //foo += myEvent;
            //foo.Invoke(new EventArgs());
            //Console.WriteLine("Watek glowny");

            RunBackgroundWorkerTcpListener();

            Console.WriteLine("Przygotowywanie macierzy");
            double[,] matrix1 = new double[10, 10];
            double[,] matrix2 = new double[10, 10];
            Random rng = new Random();

            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.GetLength(1); j++)
                {
                    matrix1[i, j] = rng.NextDouble()*5;
                    matrix2[i, j] = rng.NextDouble() * 5;
                }
            }


            MatMulCalculator matMulCalculator = new MatMulCalculator();
            Guid taskid = Guid.Empty;
            matMulCalculator.MatMulAsync(matrix1, matrix2, taskid);



            Console.WriteLine("Wątek glowny nie ma co robic");
            Thread.Sleep(10000);











            //MatMulClass matMulClass = new MatMulClass();

        }
    }
}
