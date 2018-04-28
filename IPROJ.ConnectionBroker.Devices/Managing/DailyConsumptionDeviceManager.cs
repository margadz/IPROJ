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
    public class DailyConsumptionDeviceManager : IDeviceManager
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IQueueWriter _queueWriter;
        private readonly TimeSpan _dayDelay = TimeSpan.FromHours(24) + TimeSpan.FromSeconds(1);
        private DateTime _readingGetTime;

        public DailyConsumptionDeviceManager(IQueueWriter queueWriter, IDeviceRepository deviceRepository, IConfigurationProvider configurationProvider)
        {
            Argument.OfWichValueShoulBeProvided(deviceRepository, nameof(deviceRepository));
            Argument.OfWichValueShoulBeProvided(queueWriter, nameof(queueWriter));
            Argument.OfWichValueShoulBeProvided(configurationProvider, nameof(configurationProvider));

            _deviceRepository = deviceRepository;
            _queueWriter = queueWriter;
            _readingGetTime = SetupConfiguration(configurationProvider);
            
        }

        public async Task ManageDevices(CancellationToken cancellationToken)
        {
            bool initialized = false;

            while (!cancellationToken.IsCancellationRequested)
            {
                var currentTime = DateTime.Now;
                if (!initialized && TimeSpan.Compare(currentTime.TimeOfDay, _readingGetTime.TimeOfDay) < 0)
                {
                    var toNextSend = _readingGetTime.TimeOfDay - currentTime.TimeOfDay;
                    await Task.Delay((int)toNextSend.TotalMilliseconds, cancellationToken);
                    await SendReadings(cancellationToken);
                    await WaitToNextDay(cancellationToken);
                }
                else
                {
                    await SendReadings(cancellationToken);
                    var sinceSetTime = currentTime.TimeOfDay - _readingGetTime.TimeOfDay;
                    var toNextSend = _dayDelay - sinceSetTime + TimeSpan.FromSeconds(1);
                    await Task.Delay((int)toNextSend.TotalMilliseconds, cancellationToken);
                }
            }
        }

        private DateTime SetupConfiguration(IConfigurationProvider configurationProvider)
        {
            var option = configurationProvider.GetOption(ConnectionBrokerConfigurations.Category, ConnectionBrokerConfigurations.DailyConsumptionGettingTime);
            var split = option.Split(':');
            return new DateTime(1900, 1, 1, int.Parse(split[0]), int.Parse(split[1]), 0);
        }

        private async Task SendReadings(CancellationToken cancellationToken)
        {
            var result = new List<DeviceReading>();
            foreach (var device in _deviceRepository.Devices)
            {
                result.Add(await device.GetTodaysConsumption());
            }

            await _queueWriter.Put(result, cancellationToken);
        }

        private async Task WaitToNextDay(CancellationToken cancellationToken)
        {
            await Task.Delay(_dayDelay, cancellationToken);
        }
    }
}
