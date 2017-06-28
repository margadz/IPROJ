using System;
using System.Collections.Generic;

namespace IPROJ.Contracts.DataModel
{
    public class Device
    {
        public Device()
        {
            DeviceReadings = new HashSet<DeviceReading>();
        }

        public Device(Guid deviceId, string name, string typeOfReading, bool isActive)
        {
            DeviceId = deviceId;
            Name = name;
            IsActive = isActive;
            TypeOfReading = typeOfReading;
            DeviceReadings = new HashSet<DeviceReading>();
        }

        public Guid DeviceId { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public string TypeOfReading { get; set; }

        public string DeviceIdString
        {
            get
            {
                return DeviceId.ToString();
            }
        }

        public virtual ICollection<DeviceReading> DeviceReadings { get; set; }
    }
}