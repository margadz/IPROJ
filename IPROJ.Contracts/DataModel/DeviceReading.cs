using System;

namespace IPROJ.Contracts.DataModel
{
    public class DeviceReading
    {
        DeviceDescription _device;

        public DeviceReading()
        {
        }

        public DeviceReading(DateTime readingTimeStamp, decimal value, Guid deviceId, ReadingType typeOfReading, ReadingCharacter readingCharacter, DeviceState? deviceState = null)
        {
            ReadingTimeStamp = readingTimeStamp;
            Value = value;
            DeviceId = deviceId;
            TypeOfReading = typeOfReading;
            ReadingCharacter = readingCharacter;
            DeviceState = deviceState;
        }

        public DateTime ReadingTimeStamp { get; set; }

        public decimal Value { get; set; }

        public ReadingCharacter ReadingCharacter { get; set; }

        public ReadingType TypeOfReading { get; set; }

        public DeviceState? DeviceState { get; set; }

        public Guid DeviceId
        {
            get { return Device.DeviceId; }
            set { Device.DeviceId = value; }
        }


        public DeviceDescription Device
        {
            get
            {
                if (_device == null)
                {
                    _device = new DeviceDescription();
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
