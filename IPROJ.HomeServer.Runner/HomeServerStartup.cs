using System;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.Autofac;
using IPROJ.Contracts;
using IPROJ.HomeServer.Autofac;
using IPROJ.HomeServer.QueueClient;

namespace IPROJ.HomeServer.Runner
{
    public class HomeServerStartup : IStartup
    {
        private Factory _factory;

        public void Dispose()
        {
            _factory?.Dispose();
        }

        public async Task Start(CancellationToken cancellationToken)
        {
            _factory = new HomeServerFactory();
            var handler = _factory.Resolve<IMessagesHandler>();
            try
            {
                Console.WriteLine("HomeServer started.");
                await handler.StartStartHandling(cancellationToken);
            }
            catch (OperationCanceledException)
            {
                return;
            }
        }
    }
}
