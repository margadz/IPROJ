using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.Contracts.Helpers;
using IPROJ.Contracts.Messaging;

namespace IPROJ.ConnectionBroker.Managing.Quering
{
    /// <summary>
    /// Collects all devices queries into single instance.
    /// </summary>
    public class CompoundDeviceQuery : IDeviceQuery
    {
        private readonly IEnumerable<IDeviceQuery> _devicequeries;

        /// <summary>
        /// Initializes instance of <see cref="CompoundDeviceQuery"/>.
        /// </summary>
        /// <param name="deviceQueries"></param>
        /// <param name="messenger"></param>
        public CompoundDeviceQuery(
            IEnumerable<IDeviceQuery> deviceQueries, 
            IMessenger messenger)
        {
            Argument.OfWichValueShoulBeProvided(deviceQueries, nameof(deviceQueries));
            if (deviceQueries.Any(manager => manager == null))
            {
                throw new ArgumentOutOfRangeException(nameof(deviceQueries));
            }
            Argument.OfWichValueShoulBeProvided(messenger, nameof(messenger));


            _devicequeries = deviceQueries;
        }

        /// <summary>
        /// Queries devices with all provided implementation of <see cref="IDeviceQuery"/>.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task QueryDevices(CancellationToken cancellationToken)
        {
            var tasks = _devicequeries.Select(manager => manager.QueryDevices(cancellationToken));

            await Task.WhenAll(tasks);
        }
    }
}
