using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.Configuration.Configurations;
using IPROJ.ConnectionBroker.DevicesManager;
using IPROJ.Contracts.ConfigurationProvider;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.Helpers;
using IPROJ.Contracts.Messaging;

namespace IPROJ.ConnectionBroker.Managing.Quering
{
    /// <summary>Queries devices in order to gather instant meassurements.</summary>
    public class InstantMessurmentsDeviceQuery : IDeviceQuery
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IMessenger _messenger;
        private TimeSpan _queryInterval;

        /// <summary>Initilizes instance of <see cref="InstantMessurmentsDeviceQuery"/>.</summary>
        /// <param name="messenger">Messsenger.</param>
        /// <param name="deviceRepository">Device repository.</param>
        /// <param name="configurationProvider">Configuration provider.</param>
        public InstantMessurmentsDeviceQuery(IMessenger messenger, IDeviceRepository deviceRepository, IConfigurationProvider configurationProvider)
        {
            Argument.OfWichValueShoulBeProvided(deviceRepository, nameof(deviceRepository));
            Argument.OfWichValueShoulBeProvided(messenger, nameof(messenger));
            Argument.OfWichValueShoulBeProvided(configurationProvider, nameof(configurationProvider));

            _queryInterval = TimeSpan.Parse(configurationProvider.GetOption(ConnectionBrokerConfigurations.Category, ConnectionBrokerConfigurations.InstanQueryInterval));
            _deviceRepository = deviceRepository;
            _messenger = messenger;
        }

        /// <inheritdoc />
        public async Task QueryDevices(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var readings = new List<DeviceReading>();
                foreach (var device in _deviceRepository.Devices)
                {
                    readings.Add(await device.GetInsantReading(cancellationToken));
                }

                await _messenger.SendReadings(readings, cancellationToken);

                await Task.Delay(_queryInterval, cancellationToken);
            }
        }
    }
}
