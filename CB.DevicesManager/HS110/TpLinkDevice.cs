using System;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;

namespace CB.DevicesManager.HS110
{
    public class HS110 : IDevice
    {
        public Task<DeviceReading> GetReading()
        {
            throw new NotImplementedException();
        }
    }
}
