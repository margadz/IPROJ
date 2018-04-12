using System;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.HomeServer.Autofac;
using IPROJ.HomeServer.QueueClient;

namespace IPROJ.HomeServer.Runner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HomeServerFactory factory = new HomeServerFactory();

            var handler = factory.Resolve<IMessagesHandler>();
            CancellationTokenSource source;

            source = new CancellationTokenSource();

            Task.Factory.StartNew(() => handler.StartListening(source.Token));

            Console.ReadKey();

            source.Cancel();
            source.Dispose();
        }
    }
}