using System.Threading;
using IPROJ;
using IPROJ.ConnectionBroker.Devices.HS110;
using IPROJ.ConnectionBroker.Devices.HS110.TcpCommunication;
using IPROJ.Contracts.DataModel;
using Moq;
using NUnit.Framework;
using FluentAssertions;
using IPROJ.ConnectionBroker.Devices.HS110.Commands;
using System;

namespace Given_instance_of.HS110Device_class
{
    [TestFixture]
    public class when_getting_readings : DeviceTests
    {
        private const string _instantResult = "{\"emeter\":{\"get_realtime\":{\"current\":0.521106,\"voltage\":295.070868,\"power\":134.625128,\"total\":1.962000,\"err_code\":0}}}";
        private string _dailyResult = "{\"emeter\":{\"get_daystat\":{\"day_list\":[{\"year\":2018,\"month\":"+ DateTime.UtcNow.Month +",\"day\":"+ DateTime.UtcNow.Day + ",\"energy\":0.103000},{\"year\":2018,\"month\":5,\"day\":2,\"energy\":0.195000},{\"year\":2018,\"month\":5,\"day\":3,\"energy\":0.141000},{\"year\":2018,\"month\":5,\"day\":4,\"energy\":0.183000},{\"year\":2018,\"month\":5,\"day\":5,\"energy\":0.715000},{\"year\":2018,\"month\":5,\"day\":6,\"energy\":1.305000}],\"err_code\":0}}}";
        private Mock<IHS110TcpConnector> _connector;
        private HS110Device _device;

        [Test]
        public void Should_call_connector_for_connection()
        {
            Thread.Sleep(50);
            _connector.Verify(_ => _.QueryDevice(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void Should_log_when_connected()
        {
            Thread.Sleep(50);
            Logger.Verify(_ => _.InformDeviceHasConnected(_device), Times.Once);
        }

        [Test]
        public void Should_get_instant_reading()
        {
            _device.GetInsantReading(CancellationToken.None).Result.Value.Should().Be(134.625128m);
        }

        [Test]
        public void Should_get_daily_reading()
        {
            _device.GetTodaysConsumption(CancellationToken.None).Result.Value.Should().Be(0.103000m);
        }

        [Test]
        public void Should_log_on_connection_error()
        {
            _connector.Setup(_ => _.QueryDevice(It.IsAny<string>())).Throws<Exception>();
            _device = new HS110Device(new DeviceDescription(), _connector.Object, Logger.Object);
            Logger.Verify(_ => _.RaiseErrorOnDeviceConnections(It.IsAny<Exception>(), _device), Times.Once);
        }

        [Test]
        public void Should_log_on_query()
        {
            Thread.Sleep(50);
            _connector.Setup(_ => _.QueryDevice(It.IsAny<string>())).Throws<Exception>();
            _device.GetTodaysConsumption(CancellationToken.None).Wait();
            Logger.Verify(_ => _.RaiseErrorOnGettingData(It.IsAny<Exception>(), _device), Times.Once);
        }

        protected override void ScenarioSetup()
        {
            _connector = new Mock<IHS110TcpConnector>(MockBehavior.Strict);
            _connector.Setup(_ => _.QueryDevice(CommandStrings.MonthStat(DateTime.UtcNow))).ReturnsAsync(_dailyResult);
            _connector.Setup(_ => _.QueryDevice(CommandStrings.Emeter)).ReturnsAsync(_instantResult);
            _device = new HS110Device(new DeviceDescription(), _connector.Object, Logger.Object);
        }
    }
}
