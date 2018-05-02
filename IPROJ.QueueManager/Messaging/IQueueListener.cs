using System;
using System.Threading;
using System.Threading.Tasks;

namespace IPROJ.Contracts.Messaging
{
    public interface IQueueListener : IDisposable
    {
        event QueueEventHandler QueueEvent;

        Task Listen(CancellationToken token);
    }
}
