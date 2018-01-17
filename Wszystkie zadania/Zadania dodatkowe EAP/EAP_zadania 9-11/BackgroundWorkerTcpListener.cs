using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EAP_zadania
{
    public class BackgroundWorkerTcpListener
    {
        BackgroundWorker backgroundWorker;
        object myLock = new object();

        public BackgroundWorkerTcpListener()
        {
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += new DoWorkEventHandler(BackgroundWorkerDoWork);
            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorkerProgressChanged);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorkerRunWorkerCompleted);
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.RunWorkerAsync();
        }

        public void CancelWorker()
        {
            backgroundWorker.CancelAsync();
        }

        private void BackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 1025);

            server.Start();

            while(true)
            {
                TcpClient client = server.AcceptTcpClient();
                byte[] buffer = new byte[64];
                client.GetStream().Read(buffer, 0, buffer.Length);
                string message = Encoding.ASCII.GetString(buffer);
                Console.WriteLine(message);
                byte[] newMsg = Encoding.ASCII.GetBytes("Otrzymano wiadomość");
                client.GetStream().Write(newMsg, 0, newMsg.Length);
                client.Close();


                backgroundWorker.ReportProgress(0, 1);

                if(backgroundWorker.CancellationPending)
                {
                    backgroundWorker.ReportProgress(100);
                    e.Cancel = true;
                    return;
                }

            }

        }

        private void BackgroundWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if((int)e.UserState==1)
            {
                lock(myLock)
                {
                    Console.WriteLine("ProgressChanged");
                }
            }
        }

        private void BackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lock(myLock)
            {
                Console.WriteLine("Completed");
            }
        }

    }
}
