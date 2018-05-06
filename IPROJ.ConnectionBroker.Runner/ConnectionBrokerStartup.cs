using System;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.Autofac;
using IPROJ.ConnectionBroker.Autofac;
using IPROJ.ConnectionBroker.Managing;
using IPROJ.Contracts;

namespace IPROJ.ConnectionBroker.Runner
{
    /// <summary>Entry point of ConnectionBroker.</summary>
    public class ConnectionBrokerStartup : IStartup
    {
        private Factory _factory;

        public void Dispose()
        {
            _factory?.Dispose();
        }


        /// <inheritdoc />
        public async Task Start(CancellationToken cancellationToken)
        {
            _factory = new ConnectioBrokerFactory();
            var manager = _factory.Resolve<IDeviceManager>();
            Console.WriteLine("ConnectionBroker started.");

            try
            {
                await manager.Manage(cancellationToken);
            }
            catch (OperationCanceledException)
            {
                return;
            }
        }
    }
}
