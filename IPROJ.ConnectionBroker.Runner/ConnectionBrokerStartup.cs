using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.Autofac;
using IPROJ.ConnectionBroker.Autofac;
using IPROJ.ConnectionBroker.Managing.Quering;
using IPROJ.Contracts;
using IPROJ.Contracts.Messaging;

namespace IPROJ.ConnectionBroker.Runner
{
    public class ConnectionBrokerStartup : IStartup
    {
        private Factory _factory;

        public void Dispose()
        {
            _factory?.Dispose();
        }

        public async Task Start(CancellationToken cancellationToken)
        {
            _factory = new ConnectioBrokerFactory();
            var instant = _factory.Resolve<IEnumerable<IDeviceQuery>>();
            var messenger = _factory.Resolve<IMessenger>();
            var compount = new CompoundDeviceQuery(instant, messenger);
            Console.WriteLine("ConnectionBroker started.");
            try
            {
                await compount.QueryDevices(cancellationToken);
            }
            catch (OperationCanceledException)
            {
                return;
            }
        }
    }
}
