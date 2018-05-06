using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.Configuration.Configurations;
using IPROJ.ConnectionBroker.DevicesManager;
using IPROJ.ConnectionBroker.Managing.Quering;
using IPROJ.Contracts;
using IPROJ.Contracts.ConfigurationProvider;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.Messaging;
using Moq;
using NUnit.Framework;

namespace IPROJ
{
    public abstract class DeviceManagerTests<TManager> where TManager : IDeviceQuery
    {
        private CancellationTokenSource _tokentSource;

        protected IEnumerable<DeviceReading> SentReadings { get; private set; }

        protected DeviceReading FirstReading { get; } = new DeviceReading();

        protected DeviceReading SecondReading { get; } = new DeviceReading();

        protected Mock<IDevice> FirstDevice { get; private set; }

        protected Mock<IDevice> SecondDevice { get; private set; }

        protected Mock<IQueueWriter> QueueWriter { get ; private set; }

        protected Mock<IDeviceRepository> DeviceRepository { get; private set; }

        protected Mock<IConfigurationProvider> ConfigurationProvider { get; private set; }

        protected Mock<IMessenger> Messenger { get; private set; }

        protected abstract TManager Manager { get; }

        protected void TheTest()
        {
            Task.Factory.StartNew(() => Manager.QueryDevices(_tokentSource.Token), _tokentSource.Token);
            Task.Delay(50, _tokentSource.Token).Wait();
        }

        [SetUp]
        public void TestSetup()
        {
            _tokentSource = new CancellationTokenSource();
            FirstDevice = new Mock<IDevice>(MockBehavior.Strict);
            SecondDevice = new Mock<IDevice>(MockBehavior.Strict);
            QueueWriter = new Mock<IQueueWriter>(MockBehavior.Strict);
            Messenger = new Mock<IMessenger>(MockBehavior.Strict);
            Messenger.Setup(_ => _.SendReadings(It.IsAny<IEnumerable<DeviceReading>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(0)).Callback<IEnumerable<DeviceReading>, CancellationToken>((readings, token) => SentReadings = readings);
            QueueWriter.Setup(_ => _.Put(It.IsAny<IEnumerable<DeviceReading>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(0)).Callback<IEnumerable<DeviceReading>, CancellationToken>((readings, token) => SentReadings = readings);
            FirstDevice.Setup(_ => _.GetInsantReading(It.IsAny<CancellationToken>())).ReturnsAsync(FirstReading);
            SecondDevice.Setup(_ => _.GetInsantReading(It.IsAny<CancellationToken>())).ReturnsAsync(SecondReading);
            FirstDevice.Setup(_ => _.GetTodaysConsumption(It.IsAny<CancellationToken>())).ReturnsAsync(FirstReading);
            SecondDevice.Setup(_ => _.GetTodaysConsumption(It.IsAny<CancellationToken>())).ReturnsAsync(SecondReading);
            DeviceRepository = new Mock<IDeviceRepository>(MockBehavior.Strict);
            DeviceRepository.SetupGet(_ => _.Devices).Returns(new[] { FirstDevice.Object, SecondDevice.Object });
            ConfigurationProvider = new Mock<IConfigurationProvider>(MockBehavior.Strict);
            ConfigurationProvider.Setup(_ => _.GetOption(ConnectionBrokerConfigurations.Category, ConnectionBrokerConfigurations.InstanQueryInterval)).Returns("00:00:01");
            ConfigurationProvider.Setup(_ => _.GetOption(ConnectionBrokerConfigurations.Category, ConnectionBrokerConfigurations.DailyConsumptionGettingTime)).Returns("22:00");
            ScenarioSetup();
        }

        public virtual void ScenarioSetup()
        {
        }

        [TearDown]
        public void ScenarioTearDown()
        {
            _tokentSource?.Cancel();
            _tokentSource?.Dispose();
        }
    }
}
