using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;

namespace IPROJ.QueueManager.Messaging
{
    public interface IQueueListener : IDisposable
    {
        event QueueEventHandler QueueEvent;

        void Listen(CancellationToken token);
    }

    public delegate void QueueEventHandler(IEnumerable<DeviceReading> reading);
}
