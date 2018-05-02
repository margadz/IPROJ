using System;
using System.Globalization;
using IPROJ.Contracts.DataModel;

namespace IPROJ.Contracts.Messaging
{
    public static class DeviceReadingSerializer
    {
        public static string SerializeDeviceReading(DeviceReading reading)
        {
            return $"{reading.ReadingTimeStamp.ToString("yyMMddhhmmss")};{reading.Value};{reading.DeviceId.ToString()};{(int)reading.TypeOfReading};{(int)reading.ReadingCharacter}";
        }

        public static DeviceReading DeserializeReading(string reading)
        {
            var split = reading.Split(';');
            if (split.Length != 5)
            {
                throw new ArgumentOutOfRangeException();
            }

             var timeStamp = DateTime.ParseExact(split[0], "yyMMddhhmmss", CultureInfo.InvariantCulture);
            var value = decimal.Parse(split[1]);
            var deviceId = Guid.Parse(split[2]);
            var type = (ReadingType)int.Parse(split[3]);
            var character = (ReadingCharacter)int.Parse(split[4]);

            return new DeviceReading(timeStamp, value, deviceId, type, character);
        }
    }
}
