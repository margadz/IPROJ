using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;

namespace HS.MSSQLRepository.Repository
{
    public interface IDataRepository
    {
        Task<List<DeviceReading>> GetAllReadingsAsync();

        Task<List<DeviceReading>> GetLastReadingsAsync();

        Task<List<DeviceReading>> GetAllReadingsOfTypeAsync(string type);

        Task<List<DeviceReading>> GetAllReadingsFromDeviceAsync(Guid deviceId);

        Task<List<Device>> GetAllDevicesAsync();

        Task<List<Device>> GetAllInactiveDevicesAsync();

        Task<List<Device>> GetAllActiveDevicesAsync();

        Task AddReadingsAsync(IEnumerable<DeviceReading> reading);

        Task AddDeviceAsync(Device device);

        Task RemoveDeviceAsync(Device device);

        Task SetDeviceActivityAsync(Device device, bool isActive);
    }
}
