using System;
using System.Collections.Generic;
using System.Text;

namespace IPROJ.Core.DataModel
{
    public class DeviceReadingType
    {
        public DeviceReadingType(Guid deviceId, DeviceReadingType readingType)
        {
            DeviceId = deviceId;
            ReadingType = readingType;
        }

        public Guid DeviceId { get; }
        public DeviceReadingType ReadingType { get; }
    }
}
