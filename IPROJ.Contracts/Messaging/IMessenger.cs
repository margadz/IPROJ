using IPROJ.Contracts.DataModel;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IPROJ.Contracts.Messaging
{
    /// <summary>Abstract real-time messenger facility.</summary>
    public interface IMessenger : IDisposable
    {
        /// <summary>Send readings.</summary>
        /// <param name="deviceReadings">Readings.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Task from operation.</returns>
        Task SendReadings(IEnumerable<DeviceReading> deviceReadings, CancellationToken cancellationToken);

        /// <summary>Send device descirptions.</summary>
        /// <param name="newDevices">Device descriptions.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Task from operation.</returns>
        Task SendNewDevices(IEnumerable<DeviceDescription> newDevices, CancellationToken cancellationToken);

        /// <summary>Raised when new device discover is reqested.</summary>
        event EventHandler OnDeviceDiscoveryRequest;
    }
}