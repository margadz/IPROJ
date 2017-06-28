using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HS.MSSQLRepository.Repository;
using IPROJ.Contracts.DataModel;

namespace HS.MSSQLRepository.Context.Repository
{
    public class DummRepository : IDataRepository
    {
        public Task AddDeviceAsync(Device device)
        {
            throw new NotImplementedException();
        }

        public Task AddReadingsAsync(IEnumerable<DeviceReading> reading)
        {
            throw new NotImplementedException();
        }

        public Task<List<Device>> GetAllActiveDevicesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Device>> GetAllDevicesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Device>> GetAllInactiveDevicesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<DeviceReading>> GetAllReadingsAsync()
        {
            var res = new List<DeviceReading>();

            res.Add(new DeviceReading(DateTime.Now, (decimal)121.1, Guid.NewGuid(), ReadingType.PowerConsumtion));

            return Task.FromResult(res);
        }

        public Task<List<DeviceReading>> GetAllReadingsFromDeviceAsync(Guid deviceId)
        {
            throw new NotImplementedException();
        }

        public Task<List<DeviceReading>> GetAllReadingsOfTypeAsync(string type)
        {
            throw new NotImplementedException();
        }

        public Task<List<DeviceReading>> GetLastReadingsAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveDeviceAsync(Device device)
        {
            throw new NotImplementedException();
        }

        public Task SetDeviceActivityAsync(Device device, bool isActive)
        {
            throw new NotImplementedException();
        }
    }
}
