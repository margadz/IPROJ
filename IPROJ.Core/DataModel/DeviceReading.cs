using System;

namespace IPROJ.Contracts.DataModel

{
    public class DeviceReading
    {
        public DeviceReading()
        {

        }

        public DeviceReading(DateTime readingTimeStamp, decimal value, Guid deviceId, ReadingType typeOfReading)
        {
            ReadingTimeStamp = readingTimeStamp;
            Value = value;
            DeviceId = deviceId;
            TypeOfReading = typeOfReading;
        }

        public DateTime ReadingTimeStamp { get; set; }
        public decimal Value { get; set; }
        public Guid DeviceId { get; set; }
        public ReadingType TypeOfReading { get; set; }

        public virtual Device Device { get; set; }
    }
}
