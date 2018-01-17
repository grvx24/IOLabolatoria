using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WlasnyProtokol
{
    class Client
    {
        string clientName;
        TcpClient client;
        IPAddress address;
        int port;

        List<string> messages = new List<string>();
        
        public Client(IPAddress address,int port, string clientName)
        {
            this.address = address;
            this.port = port;
            this.clientName = clientName;
        }


        public void Connect()
        {
            client = new TcpClient();
            client.Connect(address, port);
            var response = SendMessage("HI"+clientName);

            response.Wait();
        }

        public void Disconnect()
        {
            if(client!=null)
            {
                SendMessage("BYE").Wait();
                client.Close();
            }
        }

        public async Task<string> SendMessage(string message)
        {
            byte[] buffer = new ASCIIEncoding().GetBytes(message);
            await client.GetStream().WriteAsync(buffer, 0, buffer.Length);       
            buffer = new byte[64];
            var t =  client.GetStream().ReadAsync(buffer, 0, buffer.Length);
            t.Wait();
            Console.WriteLine(clientName + " otrzymuje " + Encoding.ASCII.GetString(buffer));

            return Encoding.ASCII.GetString(buffer);


        }

        public async Task<IEnumerable<string>> KeepSending(string message, CancellationTokenSource token)
        {
            List<string> messages = new List<string>();
            bool done = false;
            while (!done)
            {
                if (token.IsCancellationRequested)
                    done = true;
                messages.Add(await SendMessage(message +clientName));

                //Thread.Sleep(1000);
                //Console.ReadKey();
            }

            return messages;
        }






    }
}
