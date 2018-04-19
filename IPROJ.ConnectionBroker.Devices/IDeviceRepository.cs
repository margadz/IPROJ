using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;

namespace IPROJ.ConnectionBroker.DevicesManager
{
    public interface IDeviceRepository : IDisposable
    {
        IEnumerable<IDevice> Devices { get; }
    }
}
