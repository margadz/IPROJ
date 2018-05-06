using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;

namespace IPROJ.Contracts.Device.Discovery
{
    /// <summary>Abstract device finder.</summary>
    public interface IDeviceFinder
    {
        /// <summary>Tries to discover devices in network.</summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of discovered devices.</returns>
        Task<IEnumerable<DeviceDescription>> Discover(CancellationToken cancellationToken);
    }
}
