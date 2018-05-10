using System;

namespace IPROJ.Contracts.DataModel
{
    /// <summary>Describes reading data from device.</summary>
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

        /// <summary>Gets or sets time of the reading.</summary>
        public DateTime ReadingTimeStamp { get; set; }

        /// <summary>Gets or sets value of the reading.</summary>
        public decimal Value { get; set; }

        /// <summary>Gets or sets reading character.</summary>
        public ReadingCharacter ReadingCharacter { get; set; }

        /// <summary>Gets or sets type of the reading.</summary>
        public ReadingType TypeOfReading { get; set; }

        /// <summary>Gets or sets state of the device.</summary>
        public DeviceState? DeviceState { get; set; }

        /// <summary>Gets or sets id of the device.</summary>
        public Guid DeviceId
        {
            get { return Device.DeviceId; }
            set { Device.DeviceId = value; }
        }

        /// <summary>Gets or sets the device.</summary>
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
