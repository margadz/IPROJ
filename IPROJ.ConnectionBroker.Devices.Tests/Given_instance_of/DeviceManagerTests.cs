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

namespace IPROJ
{
    public abstract class DeviceManagerTests<TManager> where TManager : IDeviceManager
    {
        private CancellationTokenSource _tokentSource;
        private IEnumerable<DeviceReading> _sentReadings;

        protected DeviceReading FirstReading { get; } = new DeviceReading();

        protected DeviceReading SecondReading { get; } = new DeviceReading();

        protected Mock<IDevice> FirstDevice { get; private set; }

        protected Mock<IDevice> SecondDevice { get; private set; }

        protected Mock<IQueueWriter> QueueWriter { get ; private set; }

        protected Mock<IDeviceRepository> DeviceRepository { get; private set; }

        protected Mock<IConfigurationProvider> ConfigurationProvider { get; private set; }

        protected abstract TManager Manager { get; }

        protected void TheTest()
        {
            Task.Factory.StartNew(async () => await Manager.ManageDevices(_tokentSource.Token), _tokentSource.Token);
            Task.Delay(50, _tokentSource.Token).Wait();
        }

        [Test]
        public void First_device_should_be_called()
        {
            FirstDevice.Verify(_ => _.GetInsantReading(), Times.Once);
        }

        [Test]
        public void Second_device_should_be_called()
        {
            FirstDevice.Verify(_ => _.GetInsantReading(), Times.Once);
        }

        [Test]
        public void Queue_writer_should_be_called()
        {
            QueueWriter.Verify(_ => _.Put(It.IsAny<IEnumerable<DeviceReading>>()), Times.Once);
        }

        [Test]
        public void Actual_readings_should_be_sent()
        {
            _sentReadings.Any(_ => ReferenceEquals(_, FirstReading));
            _sentReadings.Any(_ => ReferenceEquals(_, SecondReading));
        }

        [SetUp]
        public void ScenarioSetup()
        {
            _tokentSource = new CancellationTokenSource();
            FirstDevice = new Mock<IDevice>(MockBehavior.Strict);
            SecondDevice = new Mock<IDevice>(MockBehavior.Strict);
            QueueWriter = new Mock<IQueueWriter>(MockBehavior.Strict);
            QueueWriter.Setup(_ => _.Put(It.IsAny<IEnumerable<DeviceReading>>())).Returns(Task.FromResult(0)).Callback<IEnumerable<DeviceReading>>(readings => _sentReadings = readings);
            FirstDevice.Setup(_ => _.GetInsantReading()).ReturnsAsync(FirstReading);
            SecondDevice.Setup(_ => _.GetInsantReading()).ReturnsAsync(SecondReading);
            DeviceRepository = new Mock<IDeviceRepository>(MockBehavior.Strict);
            DeviceRepository.SetupGet(_ => _.Devices).Returns(new[] { FirstDevice.Object, SecondDevice.Object });
            ConfigurationProvider = new Mock<IConfigurationProvider>(MockBehavior.Strict);
            ConfigurationProvider.Setup(_ => _.GetOption(ConnectionBrokerConfigurations.Category, ConnectionBrokerConfigurations.InstanQueryInterval)).Returns("00:00:01");
            ConfigurationProvider.Setup(_ => _.GetOption(ConnectionBrokerConfigurations.Category, ConnectionBrokerConfigurations.DailyConsumptionGettingTime)).Returns("22:00");
        }

        [TearDown]
        public void ScenarioTearDown()
        {
            _tokentSource?.Cancel();
            _tokentSource?.Dispose();
        }
    }
}
