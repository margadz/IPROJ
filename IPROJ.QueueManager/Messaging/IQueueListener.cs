using System;
using System.Threading.Tasks;

namespace IPROJ.QueueManager.Messaging
{
    public interface IQueueListener : IDisposable
    {
        event EventHandler QueueEvent;

        Task Listen();
    }
}
