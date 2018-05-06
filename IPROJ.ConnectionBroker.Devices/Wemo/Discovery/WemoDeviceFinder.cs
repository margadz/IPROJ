using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.Device.Discovery;

namespace IPROJ.ConnectionBroker.Devices.Wemo.Discovery
{
    /// <summary>Discovers Wemo devices.</summary>
    public class WemoDeviceFinder : IDeviceFinder
    {
        /// <inheritdoc />
        public Task<IEnumerable<DeviceDescription>> Discover(CancellationToken cancellationToken)
        {
            return Task.FromResult<IEnumerable<DeviceDescription>>(Array.Empty<DeviceDescription>());
        }
    }
}
