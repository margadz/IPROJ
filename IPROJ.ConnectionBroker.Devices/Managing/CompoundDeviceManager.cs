using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.Contracts.Helpers;

namespace IPROJ.ConnectionBroker.Devices.Managing
{
    public class CompoundDeviceManager : IDeviceManager
    {
        private readonly IEnumerable<IDeviceManager> _deviceManagers;

        public CompoundDeviceManager(IEnumerable<IDeviceManager> deviceManagers)
        {
            Argument.OfWichValueShoulBeProvided(deviceManagers, nameof(deviceManagers));
            if (deviceManagers.Any(manager => manager == null))
            {
                throw new ArgumentOutOfRangeException(nameof(deviceManagers));
            }

            _deviceManagers = deviceManagers;
        }

        public async Task ManageDevices(CancellationToken cancellationToken)
        {
            var tasks = _deviceManagers.Select(manager => manager.ManageDevices(cancellationToken));

            await Task.WhenAll(tasks);
        }
    }
}
