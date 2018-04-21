using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.ConnectionBroker.DevicesManager.HS110;
using IPROJ.ConnectionBroker.DevicesManager.Wemo;
using IPROJ.Contracts.DataRepository;
using IPROJ.Contracts.Helpers;

namespace IPROJ.ConnectionBroker.DevicesManager
{
    public sealed class DeviceRepository : IDeviceRepository
    {
        private ManualResetEvent _syncEvent = new ManualResetEvent(false);
        private readonly IDataRepository _dataRepository;
        private IEnumerable<IDevice> _devices;

        public DeviceRepository(IDataRepository deviceRepository)
        {
            Argument.OfWichValueShoulBeProvided(deviceRepository, nameof(deviceRepository));

            _dataRepository = deviceRepository;
            Task.Factory.StartNew(async () => await CollectDevices());
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
                    if (dev.TypeOfDevice.ToLower() == "hs110")
                    {
                        result.Add(new HS110Device(dev));
                    }
                    if (dev.TypeOfDevice.ToLower() == "wemo")
                    {
                        result.Add(new WemoDevice(dev));
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
