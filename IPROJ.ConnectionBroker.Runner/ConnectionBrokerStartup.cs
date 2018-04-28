using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.Autofac;
using IPROJ.ConnectionBroker.Autofac;
using IPROJ.ConnectionBroker.Devices.Managing;
using IPROJ.Contracts;

namespace IPROJ.ConnectionBroker.Runner
{
    public class ConnectionBrokerStartup : IStartup
    {
        private Factory _factory;

        public void Dispose()
        {
            _factory?.Dispose();
        }

        public Task Start(CancellationToken cancellationToken)
        {
            _factory = new ConnectioBrokerFactory();

            var instant = _factory.Resolve<IEnumerable<IDeviceManager>>();
            var compount = new CompoundDeviceManager(instant);
            Console.WriteLine("ConnectionBroker started.");
            return Task.Factory.StartNew(() => compount.ManageDevices(cancellationToken), cancellationToken);
        }
    }
}
