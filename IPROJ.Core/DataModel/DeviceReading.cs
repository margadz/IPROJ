using System;

namespace IPROJ.Contracts.DataModel

{
    public class DeviceReading
    {
        public DeviceReading(DateTime readingTimeStamp, decimal value, Guid deviceId, ReadingType typeOfReading)
        {
            ReadingTimeStamp = readingTimeStamp;
            Value = value;
            DeviceId = deviceId;
            TypeOfReading = typeOfReading;
        }

        public DateTime ReadingTimeStamp { get; private set; }
        public decimal Value { get; private set; }
        public Guid DeviceId { get; private set; }
        public ReadingType TypeOfReading { get; private set; }
    }
}
