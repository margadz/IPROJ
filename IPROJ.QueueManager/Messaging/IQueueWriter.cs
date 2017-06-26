using System;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;

namespace IPROJ.QueueManager
{
    public interface IQueueWriter : IDisposable
    {
        Task Put(DeviceReading message);
    }
}
