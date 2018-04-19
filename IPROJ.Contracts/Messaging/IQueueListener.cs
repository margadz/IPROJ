using System;
using System.Collections.Generic;
using System.Threading;
using IPROJ.Contracts.DataModel;

namespace IPROJ.Contracts.Messaging
{
    public interface IQueueListener : IDisposable
    {
        event QueueEventHandler QueueEvent;

        void Listen(CancellationToken token);
    }
}
