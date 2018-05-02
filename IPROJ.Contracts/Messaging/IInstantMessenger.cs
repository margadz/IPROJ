using IPROJ.Contracts.DataModel;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IPROJ.Contracts.Messaging
{
    public interface IInstantMessenger : IDisposable
    {
        Task SendMessage(IEnumerable<DeviceReading> deviceReadings, CancellationToken cancellationToken);
    }
}