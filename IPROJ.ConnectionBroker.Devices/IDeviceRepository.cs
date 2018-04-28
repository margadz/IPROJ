using System;
using System.Collections.Generic;
using IPROJ.Contracts;

namespace IPROJ.ConnectionBroker.DevicesManager
{
    public interface IDeviceRepository : IDisposable
    {
        IEnumerable<IDevice> Devices { get; }
    }
}
