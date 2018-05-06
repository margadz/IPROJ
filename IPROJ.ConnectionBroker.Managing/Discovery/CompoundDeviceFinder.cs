using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.Device.Discovery;
using IPROJ.Contracts.Helpers;

namespace IPROJ.ConnectionBroker.Managing.Discovery
{
    /// <summary>Uses all regiestered implementations of <see cref="IDeviceFinder"/>.</summary>
    public class CompoundDeviceFinder : IDeviceFinder
    {
        private readonly IEnumerable<IDeviceFinder> _deviceFinders;

        public CompoundDeviceFinder(IEnumerable<IDeviceFinder> deviceFinders)
        {
            Argument.OfWichValueShoulBeProvided(deviceFinders, nameof(deviceFinders));

            _deviceFinders = deviceFinders;
        }

        public async Task<IEnumerable<DeviceDescription>> Discover(CancellationToken cancellationToken)
        {
            IEnumerable<DeviceDescription> result = new List<DeviceDescription>();
            foreach(var finder in _deviceFinders)
            {
                result = result.Concat(await finder.Discover(cancellationToken));
            }

            return result;
        }
    }
}
