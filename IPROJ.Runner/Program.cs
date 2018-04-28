using System;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.ConnectionBroker.Runner;
using IPROJ.Contracts;
using IPROJ.HomeServer.Runner;

namespace IPROJ.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            var source = new CancellationTokenSource();
            var list = new IStartup[] { new ConnectionBrokerStartup(), new HomeServerStartup() };

            foreach (var startup in list)
            {
                Task.Factory.StartNew(() => startup.Start(source.Token), source.Token);
            }

            Console.ReadKey();

            source.Cancel();
            source.Dispose();
            foreach (var startup in list)
            {
                startup.Dispose();
            }
        }
    }
}
