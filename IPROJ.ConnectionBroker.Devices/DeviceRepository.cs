using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.Contracts;
using IPROJ.Contracts.DataRepository;
using IPROJ.Contracts.Devices;
using IPROJ.Contracts.Helpers;
using IPROJ.Contracts.Logging;

namespace IPROJ.ConnectionBroker.DevicesManager
{
    public sealed class DeviceRepository : IDeviceRepository
    {
        private readonly ManualResetEvent _syncEvent = new ManualResetEvent(false);
        private readonly IDataRepository _dataRepository;
        private readonly IDeviceFactory _deviceFactory;
        private IEnumerable<IDevice> _devices;

        public DeviceRepository(IDataRepository dataRepository, IDeviceFactory deviceFactory)
        {
            Argument.OfWichValueShoulBeProvided(dataRepository, nameof(dataRepository));
            Argument.OfWichValueShoulBeProvided(deviceFactory, nameof(deviceFactory));

            _dataRepository = dataRepository;
            _deviceFactory = deviceFactory;
            Task.Run(CollectDevices);
        }

        public IEnumerable<IDevice> Devices
        {
            get
            {
                _syncEvent.WaitOne();
                return _devices;
            }
        }

        public void Dispose()
        {
            foreach (var device in Devices)
            {
                device?.Dispose();
            }

            _syncEvent.Dispose();
            GC.SuppressFinalize(this);
        }

        private async Task CollectDevices()
        {
            var rawDevices = await _dataRepository.GetAllDevicesAsync();
            var result = new List<IDevice>();

            foreach (var dev in rawDevices)
            {
                result.Add(_deviceFactory.CreateDevice(dev));
            }

            _devices = result;
            _syncEvent.Set();
        }
    }
}
