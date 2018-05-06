using IPROJ.Contracts.DataModel;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IPROJ.Contracts.Messaging
{
    public interface IMessenger : IDisposable
    {
        Task SendReadings(IEnumerable<DeviceReading> deviceReadings, CancellationToken cancellationToken);

        event EventHandler OnDeviceDiscoveryRequest;
    }
}