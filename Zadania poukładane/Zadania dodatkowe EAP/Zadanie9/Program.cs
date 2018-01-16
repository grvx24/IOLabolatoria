using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EAP_zadania
{
    class Program
    {
        delegate void EventHandler(EventArgs args);
        static event EventHandler foo;

        static void MatrixMultiplication(EventArgs args)
        {
            Console.WriteLine("Pozdrowienia z glownego ciala zdarzenia");
        }

        static void myEvent(EventArgs args)

        {
            Console.WriteLine("i z ciala zaimplementowanego w ramach klienta oprogramowania");
        }

        static void Main(string[] args)
        {

            //foo = new EventHandler(MatrixMultiplication);
            //foo += myEvent;
            //foo.Invoke(new EventArgs());
            //Console.WriteLine("Watek glowny");

            Console.WriteLine("Przygotowywanie macierzy");
            double[,] matrix1 = new double[1000, 1000];
            double[,] matrix2 = new double[1000, 1000];
            Random rng = new Random();

            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.GetLength(1); j++)
                {
                    matrix1[i, j] = rng.NextDouble();
                }
            }


            MatMulCalculator matMulCalculator = new MatMulCalculator();
            Guid taskid = Guid.Empty;
            matMulCalculator.MatMulAsync(matrix1, matrix1, taskid);



            Console.WriteLine("Wątek glowny nie ma co robic");
            Thread.Sleep(10000);











            //MatMulClass matMulClass = new MatMulClass();

        }
    }
}
