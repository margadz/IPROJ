using System;

namespace IPROJ.Contracts.DataModel
{
    public class DeviceReading
    {
        Device _device;

        public DeviceReading()
        {
        }

        public DeviceReading(DateTime readingTimeStamp, decimal value, Guid deviceId, ReadingType typeOfReading, ReadingCharacter readingCharacter)
        {
            ReadingTimeStamp = readingTimeStamp;
            Value = value;
            DeviceId = deviceId;
            TypeOfReading = typeOfReading;
            ReadingCharacter = readingCharacter;
        }

        public DateTime ReadingTimeStamp { get; set; }

        public decimal Value { get; set; }

        public ReadingCharacter ReadingCharacter { get; set; }

        public ReadingType TypeOfReading { get; set; }

        public Guid DeviceId
        {
            get { return Device.DeviceId; }
            set { Device.DeviceId = value; }
        }


        public Device Device
        {
            get
            {
                if (_device == null)
                {
                    _device = new Device();
                }

                return _device;
            }

            set
            {
                _device = value;
            }
        }
    }
}
