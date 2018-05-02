using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;

namespace IPROJ.Contracts
{
    public interface IQueueWriter : IDisposable
    {
        Task Put(IEnumerable<DeviceReading> message, CancellationToken  cancellationToken);
    }
}
