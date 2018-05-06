using System;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.ConnectionBroker.Managing.Quering;
using IPROJ.Contracts.Device.Discovery;
using IPROJ.Contracts.Helpers;
using IPROJ.Contracts.Messaging;

namespace IPROJ.ConnectionBroker.Managing
{
    /// <summary>Default implementation of <see cref="IDeviceManager"/>.</summary>
    public class DeviceManager : IDeviceManager
    {
        private readonly IDeviceQuery _deviceQuery;
        private readonly IMessenger _messenger;
        private readonly IDeviceFinder _deviceFinder;
        private int counter;

        /// <summary>Initializes new instnces of <see cref="DeviceManager"/>.</summary>
        /// <param name="deviceQuery">Device query.</param>
        /// <param name="messenger">Messenger.</param>
        /// <param name="deviceFinder">Devoice finder.</param>
        public DeviceManager(IDeviceQuery deviceQuery, IMessenger messenger, IDeviceFinder deviceFinder)
        {
            Argument.OfWichValueShoulBeProvided(deviceQuery, nameof(deviceQuery));
            Argument.OfWichValueShoulBeProvided(messenger, nameof(messenger));
            Argument.OfWichValueShoulBeProvided(deviceFinder, nameof(deviceFinder));

            _deviceQuery = deviceQuery;
            _messenger = messenger;
            _deviceFinder = deviceFinder;
            _messenger.OnDeviceDiscoveryRequest += FindDevices;
        }
        
        /// <inheritdoc />
        public Task Manage(CancellationToken cancellationToken)
        {
            var task = Task.Run(async () => await _deviceQuery.QueryDevices(cancellationToken));

            return task;
        }

        private void FindDevices(object sender, EventArgs args)
        {
            if (counter++ < 1)
            {
                return;
            }

            var result = _deviceFinder.Discover(CancellationToken.None).Result;
            _messenger.SendNewDevices(result, CancellationToken.None);
        }
    }
}
