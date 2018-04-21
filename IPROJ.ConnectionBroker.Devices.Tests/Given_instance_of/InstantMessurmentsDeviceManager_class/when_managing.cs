using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.Configuration.Configurations;
using IPROJ.ConnectionBroker.Devices.Managing;
using IPROJ.ConnectionBroker.DevicesManager;
using IPROJ.Contracts;
using IPROJ.Contracts.ConfigurationProvider;
using IPROJ.Contracts.DataModel;
using Moq;
using NUnit.Framework;

namespace IPROJ.Given_instance_of.InstantMessurmentsDeviceManager_class
{
    [TestFixture]
    public class when_managing
    {
        private readonly DeviceReading _firstReading = new DeviceReading();
        private readonly DeviceReading _secondReading = new DeviceReading();
        private Mock<IDevice> _firstDevice;
        private Mock<IDevice> _secondDevice;
        private Mock<IQueueWriter> _queueWriter;
        private Mock<IDeviceRepository> _deviceRepository;
        private Mock<IConfigurationProvider> _configurationProvider;
        private InstantMessurmentsDeviceManager _manager;
        private CancellationTokenSource _tokentSource;
        private IEnumerable<DeviceReading> _sentReadings;


        public void TheTest()
        {
            Task.Factory.StartNew(async () => await _manager.ManageDevices(_tokentSource.Token), _tokentSource.Token);
            Task.Delay(50, _tokentSource.Token).Wait();
        }

        [Test]
        public void First_device_should_be_called()
        {
            _firstDevice.Verify(_ => _.GetInsantReading(), Times.Once);
        }

        [Test]
        public void Second_device_should_be_called()
        {
            _firstDevice.Verify(_ => _.GetInsantReading(), Times.Once);
        }

        [Test]
        public void Queue_writer_should_be_called()
        {
            _queueWriter.Verify(_ => _.Put(It.IsAny<IEnumerable<DeviceReading>>()), Times.Once);
        }

        [Test]
        public void Actual_readings_should_be_sent()
        {
            _sentReadings.Any(_ => ReferenceEquals(_, _firstReading));
            _sentReadings.Any(_ => ReferenceEquals(_, _secondReading));
        }

        [SetUp]
        public void ScenarioSetup()
        {
            _tokentSource = new CancellationTokenSource();
            _firstDevice = new Mock<IDevice>(MockBehavior.Strict);
            _secondDevice = new Mock<IDevice>(MockBehavior.Strict);
            _queueWriter = new Mock<IQueueWriter>(MockBehavior.Strict);
            _queueWriter.Setup(_ => _.Put(It.IsAny<IEnumerable<DeviceReading>>())).Returns(Task.FromResult(0)).Callback<IEnumerable<DeviceReading>>(readings => _sentReadings = readings);
            _firstDevice.Setup(_ => _.GetInsantReading()).ReturnsAsync(_firstReading);
            _secondDevice.Setup(_ => _.GetInsantReading()).ReturnsAsync(_secondReading);
            _deviceRepository = new Mock<IDeviceRepository>(MockBehavior.Strict);
            _deviceRepository.SetupGet(_ => _.Devices).Returns(new[] { _firstDevice.Object, _secondDevice.Object });
            _configurationProvider = new Mock<IConfigurationProvider>(MockBehavior.Strict);
            _configurationProvider.Setup(_ => _.GetOption(ConnectionBrokerConfigurations.Category, ConnectionBrokerConfigurations.InstanQueryInterval)).Returns("00:00:01");
            _manager = new InstantMessurmentsDeviceManager(_queueWriter.Object, _deviceRepository.Object, _configurationProvider.Object);
            TheTest();
        }

        [TearDown]
        public void ScenarioTearDown()
        {
            _tokentSource?.Cancel();
            _tokentSource?.Dispose();
        }
    }
}
