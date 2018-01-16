using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InzynieriaOprogramowaniaLab2
{
    class Program
    {

        static AutoResetEvent event1 = new AutoResetEvent(false);
        static void ReadCallback(IAsyncResult state)
        {
            FileStream fs = ((object[])(state.AsyncState))[0] as FileStream;
            byte[] buffer = ((object[])(state.AsyncState))[1] as byte[];

            int bytesRead = fs.EndRead(state);

            Console.WriteLine(Encoding.ASCII.GetString(buffer,0,bytesRead));
            event1.Set();

        }

        static void Zadanie6() 
        {

            FileStream  fs = new FileStream("test.txt", FileMode.Open);

            byte[] buffer= new byte[fs.Length]; ;


            fs.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(ReadCallback),new object[] { fs, buffer });          

        }


        static void Main(string[] args)
        {
            Zadanie6();
            event1.WaitOne();
        }
    }
}
