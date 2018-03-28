using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;

namespace IPROJ.HomeServer.MSSQLRepository
{
    public interface IDataRepository
    {
        Task<IEnumerable<DeviceReading>> GetAllReadingsAsync();

        Task<IEnumerable<DeviceReading>> GetAllReadingsFromDeviceAsync(Guid deviceId);

        Task<IEnumerable<Device>> GetAllDevicesAsync();

        Task AddReadingsAsync(IEnumerable<DeviceReading> reading);
    }
}
