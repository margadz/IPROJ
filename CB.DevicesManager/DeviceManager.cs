using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CB.ClientRestApi;
using CB.DevicesManager.HS110;
using IPROJ.Contracts.DataModel;
using IPROJ.QueueManager;

namespace CB.DevicesManager
{
    public class DeviceManager : IDeviceManager
    {
        private readonly IDevicesRepository _deviceRepository;
        private readonly IQueueWriter _queueWriter;
        private readonly Timer _timer;
        private IEnumerable<IDevice> _devices;

        public DeviceManager(IDevicesRepository deviceRepository, IQueueWriter queueWriter)
        {
            _deviceRepository = deviceRepository;
            _queueWriter = queueWriter;
            _devices = CollectDevices().Result;

            _timer = new Timer(EnqueueMessages, null, 0, 2000);
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

        private void EnqueueMessages(object state)
        {
            _queueWriter.Put(AquireInstantReadings().Result).Wait();
        }
    }
}
