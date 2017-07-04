using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;

namespace IPROJ.QueueManager
{
    public interface IQueueWriter : IDisposable
    {
        Task Put(IEnumerable<DeviceReading> message);
    }
}
