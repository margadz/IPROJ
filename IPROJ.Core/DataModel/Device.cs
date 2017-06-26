using System;

namespace IPROJ.Contracts.DataModel
{
    public class Device
    {
        public Device(Guid deviceId, string name, ReadingType typeOfReading, bool isActive)
        {
            DeviceId = deviceId;
            Name = name;
            IsActive = isActive;
        }

        public Guid DeviceId { get; private set; }
        public string Name { get; private set; }
        public int ReadingInterval { get; private set; }
        public bool IsActive { get; private set; }
        public string DeviceIdString
        {
            get
            {
                return DeviceId.ToString();
            }
        }
    }
}