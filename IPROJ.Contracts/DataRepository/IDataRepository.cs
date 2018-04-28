using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;

namespace IPROJ.Contracts.DataRepository
{
    public interface IDataRepository
    {
        Task<IEnumerable<DeviceReading>> GetAllReadingsAsync();

        Task<IEnumerable<DeviceReading>> GetAllReadingsFromDeviceAsync(Guid deviceId);

        Task<IEnumerable<DeviceDescription>> GetAllDevicesAsync();

        Task AddReadingsAsync(IEnumerable<DeviceReading> reading);

        Task AddDeviceAync(DeviceDescription device);
    }
}
