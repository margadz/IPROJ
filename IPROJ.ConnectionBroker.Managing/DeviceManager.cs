using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.ConnectionBroker.DevicesManager;
using IPROJ.ConnectionBroker.Managing.Quering;
using IPROJ.Contracts.DataModel;
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
        private readonly IDeviceRepository _deviceRepository;
        private int counter;

        /// <summary>Initializes new instnces of <see cref="DeviceManager"/>.</summary>
        /// <param name="deviceQuery">Device query.</param>
        /// <param name="messenger">Messenger.</param>
        /// <param name="deviceFinder">Devoice finder.</param>
        /// <param name="deviceRepository">Device repository.</param>
        public DeviceManager(IDeviceQuery deviceQuery, IMessenger messenger, IDeviceFinder deviceFinder, IDeviceRepository deviceRepository)
        {
            Argument.OfWichValueShoulBeProvided(deviceQuery, nameof(deviceQuery));
            Argument.OfWichValueShoulBeProvided(messenger, nameof(messenger));
            Argument.OfWichValueShoulBeProvided(deviceFinder, nameof(deviceFinder));
            Argument.OfWichValueShoulBeProvided(deviceRepository, nameof(deviceRepository));

            _deviceQuery = deviceQuery;
            _messenger = messenger;
            _deviceFinder = deviceFinder;
            _deviceRepository = deviceRepository;
            _messenger.OnDeviceDiscoveryRequest += FindDevices;
            _messenger.OnStateSetChangeRequest += SetDeviceState;
        }
        
        /// <inheritdoc />
        public Task Manage(CancellationToken cancellationToken)
        {
            var task = Task.Run(async () => await _deviceQuery.QueryDevices(cancellationToken));

            return task;
        }

        public void SetDeviceState(object sender, DeviceReading deviceReading)
        {
            if (deviceReading.DeviceState == null)
            {
                return;
            }

            var device = _deviceRepository.Devices.Where(reading => reading.DeviceId == reading.DeviceId).FirstOrDefault();
            device.SetState(deviceReading.DeviceState.Value);
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
