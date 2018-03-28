using System;
using IPROJ.Contracts.DataModel;

namespace IPROJ.Contracts.Messaging
{
    public static class DeviceReadingSerializer
    {
        public static string SerializeDeviceReading(DeviceReading reading)
        {
            return $"{reading.ReadingTimeStamp.ToString()};{reading.Value};{reading.DeviceId.ToString()};{reading.TypeOfReading}";
        }

        public static DeviceReading DeserializeReading(string reading)
        {
            var split = reading.Split(';');
            if (split.Length != 4)
            {
                throw new ArgumentOutOfRangeException();
            }

            return new DeviceReading(DateTime.Parse(split[0]), decimal.Parse(split[1]), Guid.Parse(split[2]), split[3]);
        }
    }
}
