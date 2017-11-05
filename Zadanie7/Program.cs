using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie7
{
    class Program
    {
        static void Zadanie7()
        {

            FileStream fs = new FileStream("test.txt", FileMode.Open);

            byte[] buffer = new byte[fs.Length];


            var ar=fs.BeginRead(buffer, 0, buffer.Length, null,null);
            //tutaj wykonywane sa obliczenia asynchronicznie 


            int i=fs.EndRead(ar);
            fs.Close();
            Console.WriteLine(Encoding.ASCII.GetString(buffer,0,i));
        }


        static void Main(string[] args)
        {
            Zadanie7();
        }
    }
}
