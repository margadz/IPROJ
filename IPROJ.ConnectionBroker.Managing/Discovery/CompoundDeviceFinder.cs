using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.Device.Discovery;
using IPROJ.Contracts.Helpers;
using IPROJ.Contracts.Logging;

namespace IPROJ.ConnectionBroker.Managing.Discovery
{
    /// <summary>Uses all regiestered implementations of <see cref="IDeviceFinder"/>.</summary>
    public class CompoundDeviceFinder : IDeviceFinder
    {
        private readonly IEnumerable<IDeviceFinder> _deviceFinders;
        private readonly IDeviceFinderLogger _logger;

        public CompoundDeviceFinder(IEnumerable<IDeviceFinder> deviceFinders, IDeviceFinderLogger logger)
        {
            Argument.OfWichValueShoulBeProvided(deviceFinders, nameof(deviceFinders));
            Argument.OfWichValueShoulBeProvided(logger, nameof(logger));

            _deviceFinders = deviceFinders;
            _logger = logger;
        }

        public async Task<IEnumerable<DeviceDescription>> Discover(CancellationToken cancellationToken)
        {
            IEnumerable<DeviceDescription> result = new List<DeviceDescription>();
            _logger.InformWhenDeviceDiscoveryHasStarted();
            foreach (var finder in _deviceFinders)
            {
                try
                {
                    result = result.Concat(await finder.Discover(cancellationToken));
                }
                catch (Exception error)
                {
                    _logger.RaiseOnErrorDuringDiscover(error, finder);
                }
            }

            _logger.InformWhenDeviceDiscoveryHasFinished(result.Count());
            return result;
        }
    }
}
