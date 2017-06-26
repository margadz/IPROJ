using System;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;

namespace IPROJ.QueueManager.Messaging
{
    public interface IQueueListener : IDisposable
    {
        Task Listen();
    }
}
