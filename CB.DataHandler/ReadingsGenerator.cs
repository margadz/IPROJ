using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using IPROJ.Contracts.DataModel;

namespace CB.DataHandling
{
    public class ReadingsGenerator
    {
        private IDevicesRepository _deviceRepository;
        private List<Device> _devices;
        private List<DeviceReading> _readings;
        private object _lock = new object();
        private static Random rand = new Random();
        private Timer timer;

        public ReadingsGenerator(IDevicesRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
            GenerateReadings(null);
            timer = new Timer(GenerateReadings, null, 0, 3000);
        }

        private IList<Device> Devices
        {
            get
            {
                if (_devices == null)
                {
                    _devices = _deviceRepository.GetAllActiveDevicesAsync().Result.Where(x => x.TypeOfReading == ReadingType.Temperature).ToList();
                }
                return _devices;
            }
        }

        public IList<DeviceReading> Readings
        {
            get
            {
                return _readings;
            }
        }

        public IList<DeviceReading> GenerateReadings()
        {
            List<DeviceReading> readings = new List<DeviceReading>(Devices.Count);

            DateTime date = DateTime.Now;

            foreach (var device in Devices)
            {
                readings.Add(new DeviceReading(date,
                                                (decimal)(20 + (rand.NextDouble() * 10)),
                                                device.DeviceId,
                                                ReadingType.Temperature));
            }

            _readings = readings;

            return Readings;
        }

        private void GenerateReadings(object state)
        {
            List<DeviceReading> readings = new List<DeviceReading>(Devices.Count);

            DateTime date = DateTime.Now;

            foreach(var device in Devices)
            {
                readings.Add(new DeviceReading(date,
                                                (decimal)(20 + (rand.NextDouble() * 10)),
                                                device.DeviceId,
                                                ReadingType.Temperature));
            }

            _readings = readings;
        }


    }
}
