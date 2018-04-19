using System;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.HomeServer.Autofac;
using IPROJ.HomeServer.QueueClient;
using IPROJ.SignalR;

namespace IPROJ.HomeServer.Runner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HomeServerFactory factory = new HomeServerFactory();

            var handler = factory.Resolve<IMessagesHandler>();
            var singalling = factory.Resolve<ISignalingDispatcher>();
            CancellationTokenSource source;

            source = new CancellationTokenSource();

            Task.Factory.StartNew(() => handler.StartStartHandling(source.Token));

            Task.Factory.StartNew(() => WebApi.Program.Main(Array.Empty<string>()));

            Task.Factory.StartNew(() => singalling.StartDispatching(source.Token));

            //var hubConnection = new HubConnectionBuilder()
            //    .WithUrl("http://localhost:12345/current")
            //    .Build();

            //hubConnection.StartAsync().Wait();

            //var rand = new Random();

            //while (true)
            //{
            //    Console.WriteLine("sending message...");
            //    hubConnection.InvokeAsync("SendMessage", rand.Next().ToString()).Wait();
            //    Thread.Sleep(500);
            //}

            Console.ReadKey();

            source.Cancel();
            source.Dispose();
        }
    }
}