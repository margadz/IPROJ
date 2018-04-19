using System.Collections.Generic;
using IPROJ.Contracts.DataModel;

namespace IPROJ.Contracts.Messaging
{
    public delegate void QueueEventHandler(IEnumerable<DeviceReading> reading);
}
