using System;

namespace IPROJ.Contracts.DataModel
{
    public class DeviceReading
    {
        private ReadingType _typeOfReading;

        public DeviceReading()
        {
        }

        public DeviceReading(DateTime readingTimeStamp, decimal value, Guid deviceId, string typeOfReading)
        {
            ReadingTimeStamp = readingTimeStamp;
            Value = value;
            DeviceId = deviceId;
            TypeOfReading = typeOfReading;
        }

        public DateTime ReadingTimeStamp { get; set; }

        public decimal Value { get; set; }

        public Guid DeviceId { get; set; }

        public string TypeOfReading
        {
            get
            {
                return _typeOfReading.ToString();
            }

            set
            {
                if (!Enum.TryParse<ReadingType>(value, out _typeOfReading))
                {
                    _typeOfReading = ReadingType.NotSpecified;
                }
            }
        }

        public virtual Device Device { get; set; }
    }
}
