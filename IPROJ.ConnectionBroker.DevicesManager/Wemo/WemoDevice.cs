using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;

namespace IPROJ.ConnectionBroker.DevicesManager.Wemo
{
    public class WemoDevice : IDevice
    {
        public Guid DeviceId => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<DeviceReading> GetDailyReading(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<DeviceReading> GetInsantReading()
        {
            throw new NotImplementedException();
        }
    }
}
