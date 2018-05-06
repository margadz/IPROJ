using IPROJ.ConnectionBroker.Devices.HS110;
using IPROJ.ConnectionBroker.Devices.HS110.TcpCommunication;
using IPROJ.ConnectionBroker.Devices.Wemo.HttpCommunication;
using IPROJ.ConnectionBroker.DevicesManager.Wemo;
using IPROJ.Contracts;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.Devices;
using IPROJ.Contracts.Helpers;
using IPROJ.Contracts.Logging;

namespace IPROJ.ConnectionBroker.Devices
{
    /// <summary>Default implementation of <see cref="IDeviceFactory"/>.</summary>
    public class DeviceFactory : IDeviceFactory
    {
        private readonly IDeviceLog _logger;

        /// <summary>Initializes instance of <see cref="DeviceFactory"/>.</summary>
        public DeviceFactory(IDeviceLog logger)
        {
            Argument.OfWichValueShoulBeProvided(logger, nameof(logger));

            _logger = logger;
        }

        public IDevice CreateDevice(DeviceDescription deviceDescription)
        {
            if (deviceDescription.TypeOfDevice == DeviceType.WEMO)
            {
                return new WemoDevice(deviceDescription, new SoapCaller(deviceDescription.Host), _logger);
            }

            var connector = new HS110TcpConnector(new TcpHost(deviceDescription.Host));
            return new HS110Device(deviceDescription, connector, _logger);
        }
    }
}
