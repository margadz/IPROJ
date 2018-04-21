using System.Threading;
using System.Threading.Tasks;
using IPROJ.ConnectionBroker.DevicesManager;
using IPROJ.Contracts;
using IPROJ.Contracts.ConfigurationProvider;
using IPROJ.Contracts.Helpers;

namespace IPROJ.ConnectionBroker.Devices.Managing
{
    public class DailyConsumptionDeviceManager : IDeviceManager
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IQueueWriter _queueWriter;

        public DailyConsumptionDeviceManager(IQueueWriter queueWriter, IDeviceRepository deviceRepository, IConfigurationProvider configurationProvider)
        {
            Argument.OfWichValueShoulBeProvided(deviceRepository, nameof(deviceRepository));
            Argument.OfWichValueShoulBeProvided(queueWriter, nameof(queueWriter));

            _deviceRepository = deviceRepository;
            _queueWriter = queueWriter;
        }

        public Task ManageDevices(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
