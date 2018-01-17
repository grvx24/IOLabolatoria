using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zadanie8
{
    class Program
    {
        delegate Int64 MyDelegate(int x);
        static MyDelegate FibIt;
        static MyDelegate FibRec;
        static MyDelegate SilniaIt;
        static MyDelegate SilniaRec;


        static Int64 SilniaRekurencja(int x)
        {
            if(x==0)
            {
                return 1;
            }else
            {
                return x * SilniaRekurencja(x - 1);
            }
            
        }

        static Int64 SilniaIteracja(int x)
        {
            Int64 result = 1;

            for (int i = 1; i <= x; i++)
            {
                result *= i;
            }
            return result;
        }


        static Int64 FibonaciRekurencja(int x)
        {

            if (x == 0) return 0;
            if (x == 1) return 1;
            return FibonaciRekurencja(x - 1) + FibonaciRekurencja(x - 2);
        }

        static Int64 FibonaciIteracja(int x)
        {
            if (x <= 1)
            {
                return x;
            }
            Int64 fib = 1;
            Int64 prevFib = 1;

            for (int i = 2; i < x; i++)
            {
                Int64 temp = fib;
                fib += prevFib;
                prevFib = temp;
            }

            return fib;
        }


        static void ThreadProc(IAsyncResult stateInfo)
        {
            MyDelegate delegat = ((object[])(stateInfo.AsyncState))[0] as MyDelegate;
            string info = ((object[])(stateInfo.AsyncState))[1] as string;
            AutoResetEvent ev= ((object[])(stateInfo.AsyncState))[2] as AutoResetEvent;

            Console.WriteLine(info+": "+ delegat.EndInvoke(stateInfo).ToString());

            ev.Set();

        }
        
        


        static void Main(string[] args)
        {
            FibIt += FibonaciIteracja;
            FibRec += FibonaciRekurencja;
            SilniaIt += SilniaIteracja;
            SilniaRec += SilniaRekurencja;

            int value = 50;

            AutoResetEvent[] events = new AutoResetEvent[4];
            for (int i = 0; i < 4; i++)
            {
                events[i] = new AutoResetEvent(false);
            }
            var res1 = FibIt.BeginInvoke(value, ThreadProc, new object[] { FibIt, "Fib iteracyjnie",events[0] });
          
            
            var res2 = FibRec.BeginInvoke(value, ThreadProc, new object[] { FibRec, "Fib rekurencyjnie", events[1] });
            
            var res3 = SilniaIt.BeginInvoke(10, ThreadProc, new object[] { SilniaIt,"Silnia iteracyjnie", events[2] });
            
            var res4 = SilniaRec.BeginInvoke(10, ThreadProc, new object[] { SilniaRec, "Silnia rekurencja", events[3] });
            

            WaitHandle.WaitAll(events);
        }
    }
}
