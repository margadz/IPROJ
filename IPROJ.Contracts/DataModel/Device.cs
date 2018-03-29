using System;
using System.Collections.Generic;

namespace IPROJ.Contracts.DataModel
{
    public class Device
    {
        public Device()
        {
            DeviceReadings = new HashSet<DeviceReading>();
            CustomId = string.Empty;
        }

        public Device(Guid deviceId, string name, string typeOfReading, bool isActive, string host, string customId = null)
        {
            DeviceId = deviceId;
            Name = name;
            IsActive = isActive;
            TypeOfReading = typeOfReading;
            Host = host;
            CustomId = customId ?? string.Empty;
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

        public string TypeOfDevice { get; set; }

        public string CustomId { get; set; }

        public string Host { get; set; }

        public virtual ICollection<DeviceReading> DeviceReadings { get; set; }
    }
}