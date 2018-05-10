using System;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;

namespace IPROJ.Contracts
{
    /// <summary>Describes abstract device.</summary>
    public interface IDevice : IDisposable
    {
        /// <summary>Gets Device Id</summary>
        Guid DeviceId { get; }

        /// <summary>Gets device name.</summary>
        string DeviceName { get; }

        /// <summary>Gets type of reading.</summary>
        ReadingType TypeOfReading { get; }

        /// <summary>Gets instant reading.</summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Instant reading.</returns>
        Task<DeviceReading> GetInsantReading(CancellationToken cancellationToken);

        /// <summary>Gets daily reading.</summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Daily reading.</returns>
        Task<DeviceReading> GetTodaysConsumption(CancellationToken cancellationToken);

        /// <summary>Change device state from on/off and vice-versa.</summary>
        /// <param name="deviceState">Desired device state.</param>
        /// <returns>Task from the operation.</returns>
        Task SetState(DeviceState deviceState);
    }
}
