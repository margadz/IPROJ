using System;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.Autofac;
using IPROJ.ConnectionBroker.Autofac;
using IPROJ.ConnectionBroker.Managing;
using IPROJ.Contracts;
using IPROJ.Contracts.Logging;

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
            var logger = _factory.Resolve<IStartupLogger>();
            logger.InformStartupProcessIsStarting("ConnectionBroker");
            var manager = _factory.Resolve<IDeviceManager>();
            logger.InformStartupProcessHasStarted("ConnectionBroker");

            try
            {
                await manager.Manage(cancellationToken);
            }
            catch (OperationCanceledException)
            {
                return;
            }
            catch (Exception error)
            {
                logger.RaiseOnErrorDuringStartupProcessStart(error, "ConnectionBroker");
            }
        }
    }
}
