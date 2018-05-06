using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;

namespace IPROJ.Contracts.Device.Discovery
{
    public interface IDeviceFinder
    {
        Task<IEnumerable<DeviceDescription>> Find(CancellationToken cancellationToken);
    }
}
