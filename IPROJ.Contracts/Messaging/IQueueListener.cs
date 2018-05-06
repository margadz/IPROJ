using System;
using System.Threading;
using System.Threading.Tasks;

namespace IPROJ.Contracts.Messaging
{
    /// <summary>Describes abstract queue listening facility.</summary>
    public interface IQueueListener : IDisposable
    {
        /// <summary>
        /// Raised when listener received a message from the queue.
        /// </summary>
        event QueueEventHandler OnMessegeReceived;

        Task Listen(CancellationToken token);
    }
}
