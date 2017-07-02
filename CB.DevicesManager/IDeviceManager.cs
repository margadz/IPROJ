using System;
using System.Collections.Generic;
using System.Text;
using IPROJ.Contracts.DataModel;

namespace CB.DevicesManager
{
    public interface IDeviceManager
    {
        IEnumerable<IDevice> Devices { get; }

        Task<IEnumerable<DeviceReading>> AquireInstantReadings();
    }
}
