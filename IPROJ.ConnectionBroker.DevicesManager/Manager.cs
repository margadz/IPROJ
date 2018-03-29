using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.ConnectionBroker.DevicesManager.HS110;
using IPROJ.Contracts;
using IPROJ.Contracts.DataModel;
using IPROJ.MSSQLRepository.Repository;

namespace IPROJ.ConnectionBroker.DevicesManager
{
    public class Manager : IDeviceManager
    {
        private readonly IDataRepository _dataRepository;
        private readonly IQueueWriter _queueWriter;
        private readonly Timer _timer;

        public Manager(IDataRepository deviceRepository, IQueueWriter queueWriter)
        {
            _dataRepository = deviceRepository;
            _queueWriter = queueWriter;
            Devices = CollectDevices().Result;

            //_timer = new Timer(EnqueueMessages, null, 100, 1000);
        }

        public IEnumerable<IDevice> Devices { get; }

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
            var rawDevices = await _dataRepository.GetAllDevicesAsync();
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
            foreach (var reading in AquireInstantReadings().Result)
            {
                System.Console.WriteLine("instant: " + reading.Value);

            }
            
            ///_queueWriter.Put(AquireInstantReadings().Result).Wait();
        }
    }
}
