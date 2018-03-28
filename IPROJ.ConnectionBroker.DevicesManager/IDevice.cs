using System;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;

namespace IPROJ.ConnectionBroker.DevicesManager
{
    public interface IDevice : IDisposable
    {
        Guid DeviceId { get; }

        Task<DeviceReading> GetInsantReading();

        Task<DeviceReading> GetDailyReading(DateTime date);
    }
}
