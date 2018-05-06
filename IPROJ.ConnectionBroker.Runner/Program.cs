using System;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.ConnectionBroker.Devices.HS110;

namespace IPROJ.ConnectionBroker.Runner
{
    class Program
    {
        public static void Main(string[] args)
        {
            var startup = new ConnectionBrokerStartup();
            var tokenSource = new CancellationTokenSource();

            Task.Factory.StartNew(() => startup.Start(tokenSource.Token), tokenSource.Token);

            Console.ReadKey();
            tokenSource.Cancel();
            tokenSource.Dispose();
            startup.Dispose();
            //Environment.Exit(0);
        }
    }
}
