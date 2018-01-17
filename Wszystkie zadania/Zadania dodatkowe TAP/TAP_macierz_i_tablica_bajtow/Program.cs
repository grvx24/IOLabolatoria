using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TAP_macierz_i_tablica_bajtow
{
    class Program
    {

        //wczytac asynchronicznie calosc IO bound
        //parsowanie zeby usunac niepotrzebne znaki - na wielu watkach cpu bound
        //dac list to array


        //IO bound
        static async Task<string> LoadMatrixAsync(string path)
        {
            using (var reader = new StreamReader(path))
            {
                string line = await reader.ReadToEndAsync();
                return line;
            }
            
        }
        //Cpu Bound
        static Task<int[]> BindMatrixToArray(string path)
        {
            var matrix =LoadMatrixAsync(path);
            //Console.WriteLine("Wczytywanie z pliku");
            matrix.Wait();


            return Task.Run(() =>
            {
                //Console.WriteLine("Operacje na wczytanym tekście i wczytywanie do zmiennej");
                string[] delimiters = { " ", ",", Environment.NewLine };
                var linesAfterSplit=matrix.Result.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                int[] array = new int[linesAfterSplit.Length];

                for (int i = 0; i < array.Length; i++)
                {
                    //Console.WriteLine(linesAfterSplit[i]);
                    array[i] = Convert.ToInt32(linesAfterSplit[i]);
                }

                Console.WriteLine("Wczytywanie macierzy zakonczone");
                return array;

            });
        }

        //lepiej nie przesadzac z wartosciami :/
        static void GenerateMatrixToFile(int r,int c, int min,int max)
        {

            
            Random rng = new Random();

            StreamWriter streamWriter = new StreamWriter("matrix.txt");

            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    streamWriter.Write(rng.Next(min, max)+" ");
                }
                streamWriter.Write(Environment.NewLine);
            }
            streamWriter.Close();
        }

        //zapis tablicy bajtów do pliku asynchronicznie
        static async Task SaveArrayToFileAsync(byte[] array, string path)
        {
            using (var writer = new FileStream(path, FileMode.OpenOrCreate))
            {
                //Console.WriteLine("Zapis asynchorniczny");
                await writer.WriteAsync(array, 0, array.Length);
                Console.WriteLine("Zapis tablicy zakończony");
            }
        }

        static void Main(string[] args)
        {
            Random rng = new Random();
            Console.WriteLine("Rozpoczecie wczytywania macierzy");
            var task = BindMatrixToArray("matrix.txt");

            //podczas gdy w tle jest wczytywana macierz, jednoczesnie inna tablica bajtów jest zapisywana do pliku
            Console.WriteLine("Rozpoczęcie zapisu tablicy bajtów");
            byte[] array = new byte[10000];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = (byte)rng.Next(0, 256);

            }
            var task2 = SaveArrayToFileAsync(array, "tablica.bin");
    
            //czekanie na task2
            //Console.WriteLine("Czekannie na zapis tablicy bajtów");
            //task2.Wait();
            //Console.WriteLine("Zapisano tablice");
            //czekanie na task
            //Console.WriteLine("Czekanie na zakonczenie wczytywania");
            //task.Wait();
            //Console.WriteLine("Wczytano macierz");

            Console.WriteLine("Czekanie na zakończenie wszystkich tasków");
            var result = task.Result;
            //zakomentowane bo wypisywanie dlugo trwa
            //foreach (var item in result)
            //{
            //    Console.WriteLine(item);
            //}

            //inna opcja to czekanie na kazdy z tasków osobno
            Task.WaitAll(new Task[] { task, task2 });
            Console.WriteLine("Koniec programu");

        }
    }
}
