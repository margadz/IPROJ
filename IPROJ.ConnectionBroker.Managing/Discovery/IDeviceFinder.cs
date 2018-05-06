using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;

namespace IIPROJ.ConnectionBroker.Managing.Discovery
{
    public interface IDeviceFinder
    {
        Task<IEnumerable<DeviceDescription>> Find(CancellationToken cancellationToken);
    }
}
