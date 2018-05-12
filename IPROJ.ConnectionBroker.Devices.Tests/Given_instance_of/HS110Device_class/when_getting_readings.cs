using System;
using System.Threading;
using FluentAssertions;
using IPROJ;
using IPROJ.ConnectionBroker.Devices.HS110;
using IPROJ.ConnectionBroker.Devices.HS110.Commands;
using IPROJ.ConnectionBroker.Devices.HS110.TcpCommunication;
using IPROJ.Contracts.DataModel;
using Moq;
using NUnit.Framework;

namespace Given_instance_of.HS110Device_class
{
    [TestFixture]
    public class when_getting_readings : DeviceTests
    {
        private const string _instantResult = "{\"emeter\":{\"get_realtime\":{\"current\":0.521106,\"voltage\":295.070868,\"power\":134.625128,\"total\":1.962000,\"err_code\":0}}}";
        private const string _sysInfo = "{\"system\":{\"get_sysinfo\":{\"err_code\":0,\"sw_ver\":\"1.1.0 Build 160503 Rel.144605\",\"hw_ver\":\"1.0\",\"type\":\"IOT.SMARTPLUGSWITCH\",\"model\":\"HS110(EU)\",\"mac\":\"50:C7:BF:0B:96:0B\",\"deviceId\":\"8006D1847073EC74595FFCD43771CB2817AFBCAD\",\"hwId\":\"45E29DA8382494D2E82688B52A0B2EB5\",\"fwId\":\"B78BB2C0C8C2A9D31A75E0CD71430A5F\",\"oemId\":\"3D341ECE302C0642C99E31CE2430544B\",\"alias\":\"plug\",\"dev_name\":\"Wi-Fi Smart Plug With Energy Monitoring\",\"icon_hash\":\"\",\"relay_state\":0,\"on_time\":915,\"active_mode\":\"schedule\",\"feature\":\"TIM:ENE\",\"updating\":0,\"rssi\":-57,\"led_off\":0,\"latitude\":50.570543,\"longitude\":22.062227}}}";
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
        public void Should_call_connector_for_instance_readings()
        {
            _device.GetInsantReading(CancellationToken.None).Wait();
            _connector.Verify(_ => _.QueryDevice(Hs110Commands.Emeter), Times.AtLeastOnce);
        }

        [Test]
        public void Should_call_connector_for_daily_readings()
        {
            _device.GetTodaysConsumption(CancellationToken.None).Wait();
            _connector.Verify(_ => _.QueryDevice(Hs110Commands.MonthStat(DateTime.UtcNow)), Times.Once);
        }

        [Test]
        public void Should_log_on_connection_error()
        {
            _connector.Setup(_ => _.QueryDevice(It.IsAny<string>())).Throws<Exception>();
            _device = new HS110Device(new DeviceDescription(), _connector.Object, Logger.Object);
            Logger.Verify(_ => _.RaiseErrorOnDeviceConnections(It.IsAny<Exception>(), _device), Times.Once);
        }

        [Test]
        public void Should_get_correct_stat()
        {
            _device.GetInsantReading(CancellationToken.None).Result.DeviceState.Should().Be(DeviceState.Off);
        }

        [Test]
        public void Should_turn_the_device_off()
        {
            _device.SetState(DeviceState.Off).Wait();
            _connector.Verify(_ => _.QueryDevice(Hs110Commands.TurnOff), Times.Once);
        }

        [Test]
        public void Should_turn_the_device_on()
        {
            _device.SetState(DeviceState.On).Wait();
            _connector.Verify(_ => _.QueryDevice(Hs110Commands.TurnOn), Times.Once);
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
            _connector.Setup(_ => _.QueryDevice(Hs110Commands.MonthStat(DateTime.UtcNow))).ReturnsAsync(_dailyResult);
            _connector.Setup(_ => _.QueryDevice(Hs110Commands.Emeter)).ReturnsAsync(_instantResult);
            _connector.Setup(_ => _.QueryDevice(Hs110Commands.SysInfo)).ReturnsAsync(_sysInfo);
            _connector.Setup(_ => _.QueryDevice(Hs110Commands.TurnOff)).ReturnsAsync(string.Empty);
            _connector.Setup(_ => _.QueryDevice(Hs110Commands.TurnOn)).ReturnsAsync(string.Empty);
            _device = new HS110Device(new DeviceDescription(), _connector.Object, Logger.Object);
        }
    }
}
