using System;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;

namespace IPROJ.Contracts
{
    public interface IDevice : IDisposable
    {
        Guid DeviceId { get; }

        string DeviceName { get; }

        Task<DeviceReading> GetInsantReading(CancellationToken cancellationToken);

        Task<DeviceReading> GetTodaysConsumption(CancellationToken cancellationToken);
    }
}
