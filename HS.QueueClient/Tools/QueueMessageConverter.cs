using HS.Common.Enumerators;
using HS.Common.Exception;
using HS.Common.OutputModel;
using System;
using System.Collections.Generic;

namespace HS.QueueClient.Tools
{
    public static class QueueMessageConverter
    {
        public static DeviceReading ParseDeviceReding(string queueMessage)
        {
            var elements = queueMessage.Split(';');
            if (elements.Length != 4)
            {
                throw new QueueMessageException($"Could not read DeviceReading from message: {queueMessage}");
            }

            DeviceReading reading = null;

            try
            {
                reading = new DeviceReading(
                    deviceId: Guid.Parse(elements[0]),
                    value: decimal.Parse(elements[1]),
                    typeOfReading: (ReadingType)Enum.Parse(typeof(ReadingType), elements[2]),
                    readingTimeStamp: DateTime.Parse(elements[3]));
            }
            catch
            {
                throw new QueueMessageException($"Could not read DeviceReading from message: {queueMessage}");
            }

            return reading;
        }

        public static IList<DeviceReading> ParseDeviceReaddings(string messsage)
        {
            List<DeviceReading> result = new List<DeviceReading>();

            var readings = messsage.Split('|');

            foreach(var read in readings)
            {
                if (!string.IsNullOrEmpty(read))
                {
                    result.Add(ParseDeviceReding(read));
                }
            }

            return result;

        }
    }
}
