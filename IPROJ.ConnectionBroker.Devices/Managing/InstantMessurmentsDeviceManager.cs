using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.Configuration.Configurations;
using IPROJ.ConnectionBroker.DevicesManager;
using IPROJ.Contracts;
using IPROJ.Contracts.ConfigurationProvider;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.Helpers;

namespace IPROJ.ConnectionBroker.Devices.Managing
{
    public class InstantMessurmentsDeviceManager : IDeviceManager
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IQueueWriter _queueWriter;
        private TimeSpan _queryInterval;

        public InstantMessurmentsDeviceManager(IQueueWriter queueWriter, IDeviceRepository deviceRepository, IConfigurationProvider configurationProvider)
        {
            Argument.OfWichValueShoulBeProvided(deviceRepository, nameof(deviceRepository));
            Argument.OfWichValueShoulBeProvided(queueWriter, nameof(queueWriter));
            Argument.OfWichValueShoulBeProvided(configurationProvider, nameof(configurationProvider));

            _queryInterval = TimeSpan.Parse(configurationProvider.GetOption(ConnectionBrokerConfigurations.Category, ConnectionBrokerConfigurations.InstanQueryInterval));
            _deviceRepository = deviceRepository;
            _queueWriter = queueWriter;
        }

        public async Task ManageDevices(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var readings = new List<DeviceReading>();
                foreach (var device in _deviceRepository.Devices)
                {
                    readings.Add(await device.GetInsantReading());
                }

                await _queueWriter.Put(readings);

                await Task.Delay(_queryInterval, cancellationToken);
            }
        }
    }
}
