using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.ConnectionBroker.DevicesManager.HS110;
using IPROJ.ConnectionBroker.DevicesManager.Wemo;
using IPROJ.Contracts;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.DataRepository;
using IPROJ.Contracts.Helpers;
using IPROJ.Contracts.Logging;

namespace IPROJ.ConnectionBroker.DevicesManager
{
    public sealed class DeviceRepository : IDeviceRepository
    {
        private readonly ManualResetEvent _syncEvent = new ManualResetEvent(false);
        private readonly IDataRepository _dataRepository;
        private readonly IDeviceLog _logger;
        private IEnumerable<IDevice> _devices;

        public DeviceRepository(IDataRepository deviceRepository, IDeviceLog logger)
        {
            Argument.OfWichValueShoulBeProvided(deviceRepository, nameof(deviceRepository));
            Argument.OfWichValueShoulBeProvided(logger, nameof(logger));

            _logger = logger;
            _dataRepository = deviceRepository;
            Task.Factory.StartNew(() => CollectDevices());
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
            foreach(var device in Devices)
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
                try
                {
                    if (dev.TypeOfDevice == DeviceType.HS110)
                    {
                        result.Add(new HS110Device(dev, _logger));
                    }
                    if (dev.TypeOfDevice == DeviceType.WEMO)
                    {
                        result.Add(new WemoDevice(dev, _logger));
                    }

                }
                catch (DeviceException)
                {
                    // Supress
                }
            }

            _devices = result;
            _syncEvent.Set();
        }
    }
}
