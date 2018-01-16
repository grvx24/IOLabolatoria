using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WlasnyProtokol
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Server server = new Server(IPAddress.Loopback,1024);
            server.Run();


            Client client1 = new Client(IPAddress.Loopback, 1024, "<Klient1>");
            Client client2 = new Client(IPAddress.Loopback, 1024, "<Klient2>");
            Client client3 = new Client(IPAddress.Loopback, 1024, "<Klient3>");
            Client client4 = new Client(IPAddress.Loopback, 1024, "<Klient4>");

            CancellationTokenSource ct1 = new CancellationTokenSource();
            CancellationTokenSource ct2 = new CancellationTokenSource();
            CancellationTokenSource ct3 = new CancellationTokenSource();
            CancellationTokenSource ct4 = new CancellationTokenSource();

            client1.Connect();
            client2.Connect();
            client3.Connect();
            client4.Connect();

            var t1 = client1.KeepSending("TAK", ct1);
            var t2 = client2.KeepSending("NIE", ct2);
            var t3 = client3.KeepSending("TAK", ct3);
            var t4 = client4.KeepSending("NIE", ct4);

            Task.WaitAll(new Task[] { t1,t2,t3,t4 });

        }
    }
}
