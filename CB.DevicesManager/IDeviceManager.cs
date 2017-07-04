using System.Collections.Generic;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;

namespace CB.DevicesManager
{
    public interface IDeviceManager
    {
        IEnumerable<IDevice> Devices { get; }

        Task<IEnumerable<DeviceReading>> AquireInstantReadings();
    }
}
