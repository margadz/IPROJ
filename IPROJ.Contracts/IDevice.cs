using System;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;

namespace IPROJ.Contracts
{
    public interface IDevice : IDisposable
    {
        Guid DeviceId { get; }

        string DeviceName { get; }

        Task<DeviceReading> GetInsantReading();

        Task<DeviceReading> GetTodaysConsumption();
    }
}
