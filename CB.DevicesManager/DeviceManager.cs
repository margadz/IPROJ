using System.Collections.Generic;
using System.Threading.Tasks;
using CB.ClientRestApi;
using CB.DevicesManager.HS110;
using IPROJ.Contracts.DataModel;

namespace CB.DevicesManager
{
    public class DeviceManager : IDeviceManager
    {
        private readonly IDevicesRepository _deviceRepository;
        private IEnumerable<IDevice> _devices;

        public DeviceManager(IDevicesRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
            _devices = CollectDevices().Result;
        }

        public IEnumerable<IDevice> Devices
        {
            get
            {
                return _devices;
            }
        }

        public async Task<IEnumerable<DeviceReading>> AquireInstantReadings()
        {
            var result = new List<DeviceReading>();
            foreach (var device in Devices)
            {
                result.Add(await device.GetInsantReading());
            }

            return result;
        }

        private async Task<IEnumerable<IDevice>> CollectDevices()
        {
            var rawDevices = await _deviceRepository.GetAllDevicesAsync();
            var result = new List<HS110Device>();

            foreach (var dev in rawDevices)
            {
                try
                {
                    result.Add(new HS110Device(dev));
                }
                catch (DeviceException)
                {
                    // Supress
                }
            }

            return result;
        }
    }
}
