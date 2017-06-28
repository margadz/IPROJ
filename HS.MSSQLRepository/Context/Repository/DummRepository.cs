using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HS.MSSQLRepository.Repository;
using IPROJ.Contracts.DataModel;

namespace HS.MSSQLRepository.Context.Repository
{
    public class DummRepository : IDataRepository
    {
        public Task AddReadingsAsync(IEnumerable<DeviceReading> reading)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Device>> GetAllActiveDevicesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Device>> GetAllDevicesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DeviceReading>> GetAllReadingsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DeviceReading>> GetAllReadingsFromDeviceAsync(Guid deviceId)
        {
            throw new NotImplementedException();
        }
    }
}
