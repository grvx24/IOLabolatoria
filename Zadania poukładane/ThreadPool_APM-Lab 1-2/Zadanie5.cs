using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace InzynieriaOprogramowaniaZadanie5
{

    class Program
    {
        public static int[] array;
        public static int result = 0;
        private static object thisLock = new object();
        private static bool tasksCompleted = false;//żeby zatrzymać maina zanim sie nie policzy :)

        class State
        {
            public int startIndex;
            public int portionSize;
            public AutoResetEvent autoEvent;
            public string threadName;

            public State(int startIndex,int portionSize,AutoResetEvent autoEvent,string name)
            {
                this.startIndex = startIndex;
                this.portionSize = portionSize;
                this.autoEvent = autoEvent;
                this.threadName = name;

            }
        }

        

        static void Zadanie5(int size,int arrayPiece)
        {

            int numOfThreads;
            int lastPiece = 0;
            if(size%arrayPiece==0)
            {
                numOfThreads = size / arrayPiece;
            }
            else
            {
                numOfThreads = (size / arrayPiece)+1;
                lastPiece = size % arrayPiece;
            }
            //Console.WriteLine("Last piece:" +lastPiece);

            Console.WriteLine("Array size: " + size);
            Console.WriteLine("Array portion size " + arrayPiece);
            Console.WriteLine("Number of threads: " + numOfThreads);



            array = new int[size];
            Random rnd = new Random();
            for(int i= 0;i < size;i++)
            {
                array[i]= rnd.Next(0, 10);
                //array[i] = 1;
            }

            foreach(var a in array)
            {
                Console.Write(a + " ");
            }
            Console.Write("\n");

            AutoResetEvent[] autoEvents = new AutoResetEvent[numOfThreads];

            for (int i = 0; i < numOfThreads; i++)
            {
                //podział na wątki

                autoEvents[i] = new AutoResetEvent(false);
                if(i==numOfThreads-1&&lastPiece!=0)
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(Adder), new State(i * arrayPiece, lastPiece, autoEvents[i],"Thread number: "+i));
                }
                else
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(Adder), new State(i * arrayPiece, arrayPiece, autoEvents[i], "Thread number: " + i));
                }
            }


            WaitHandle.WaitAll(autoEvents);
            tasksCompleted = true;
            Console.WriteLine("All threads completed");


        }

        static void Adder(object stateInfo)
        {
            int sum = 0;
            State state = stateInfo as State;


            lock (thisLock)
            {
                for (int i = state.startIndex; i < state.startIndex + state.portionSize; i++)
                {
                    sum += array[i];
                }
                result += sum;
            }
            //Console.WriteLine(DateTime.Now.Millisecond);
            Console.WriteLine(state.threadName + " result:" + sum);
            state.autoEvent.Set();

        }

        static void Main(string[] args)
        {

            Zadanie5(500, 10);

            while(!tasksCompleted)
            {

            }
            //Thread.Sleep(10);
            Console.WriteLine("Result of all threads: "+result);

        }
    }
}
