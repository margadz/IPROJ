using System;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.Autofac;
using IPROJ.Contracts;
using IPROJ.HomeServer.Autofac;
using IPROJ.HomeServer.QueueClient;
using IPROJ.HomeServer.SignalR;

namespace IPROJ.HomeServer.Runner
{
    public class HomeServerStartup : IStartup
    {
        private Factory _factory;

        public void Dispose()
        {
            _factory?.Dispose();
        }

        public Task Start(CancellationToken cancellationToken)
        {
            _factory = new HomeServerFactory();

            var handler = _factory.Resolve<IMessagesHandler>();
            var singalling = _factory.Resolve<ISignalingDispatcher>();

            Task.Factory.StartNew(() => handler.StartStartHandling(cancellationToken), cancellationToken);

            /*Task.Factory.StartNew(() => HomeServer.WebApi.Program.Main(Array.Empty<string>()), cancellationToken);*/

            Task.Factory.StartNew(() => singalling.StartDispatching(cancellationToken), cancellationToken);
            Console.WriteLine("HomeServer started.");
            return Task.FromResult(0);
        }
    }
}
