﻿using System;

namespace IPROJ.Contracts.DataModel
{
    public class DeviceDescription
    {
        public DeviceDescription()
        {
            CustomId = string.Empty;
        }

        public DeviceDescription(Guid deviceId, string name, ReadingType typeOfReading, bool isActive, string host, string typeOfDevice, string customId = null)
        {
            DeviceId = deviceId;
            Name = name;
            IsActive = isActive;
            TypeOfReading = typeOfReading;
            Host = host;
            CustomId = customId ?? string.Empty;
            TypeOfDevice = typeOfDevice;
        }

        public Guid DeviceId { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public ReadingType TypeOfReading { get; set; }

        public string TypeOfDevice { get; set; }

        public string CustomId { get; set; }

        public string Host { get; set; }
    }
}