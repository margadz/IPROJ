using System;
using System.Threading;
using FluentAssertions;
using IPROJ;
using IPROJ.ConnectionBroker.Devices.Wemo.Commands;
using IPROJ.ConnectionBroker.Devices.Wemo.HttpCommunication;
using IPROJ.ConnectionBroker.DevicesManager.Wemo;
using IPROJ.Contracts.DataModel;
using Moq;
using NUnit.Framework;

namespace Given_instance_of.WemoDevice_class
{
    [TestFixture]
    public class when_getting_readings : DeviceTests
    {
        private const string _response = "<InsightParams>0|1525883173|6692|6972|6831|952919|98|94100|13667836|13667836.000000|8000</InsightParams>";
        private WemoDevice _device;
        private Mock<ISoapCaller> _soapCaller;

        [Test]
        public void Should_call_connector_for_connection()
        {
            Thread.Sleep(50);
            _soapCaller.Verify(_ => _.SendRequest(It.IsAny<IWemoCommand>()), Times.Once);
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
            _device.GetInsantReading(CancellationToken.None).Result.Value.Should().Be(94.1m);
        }

        [Test]
        public void Should_get_daily_reading()
        {
            _device.GetTodaysConsumption(CancellationToken.None).Result.Value.Should().BeInRange(0.227m, 0.228m);
        }

        [Test]
        public void Should_call_connector_for_instance_readings()
        {
            _device.GetInsantReading(CancellationToken.None).Wait();
            _soapCaller.Verify(_ => _.SendRequest(It.IsAny<GetInsightParamsWemoCommand>()), Times.AtLeastOnce);
        }

        [Test]
        public void Should_call_connector_for_daily_readings()
        {
            _device.GetTodaysConsumption(CancellationToken.None).Wait();
            _soapCaller.Verify(_ => _.SendRequest(It.IsAny<GetInsightParamsWemoCommand>()), Times.AtLeastOnce);
        }

        [Test]
        public void Should_log_on_connection_error()
        {
            _soapCaller.Setup(_ => _.SendRequest(It.IsAny<IWemoCommand>())).Throws<Exception>();
            _device = new WemoDevice(new DeviceDescription(), _soapCaller.Object, Logger.Object);
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
            _soapCaller.Verify(_ => _.SendRequest(SetInsightStateCommand.Off), Times.Once);
        }

        [Test]
        public void Should_turn_the_device_on()
        {
            _device.SetState(DeviceState.On).Wait();
            _soapCaller.Verify(_ => _.SendRequest(SetInsightStateCommand.On), Times.Once);
        }

        [Test]
        public void Should_log_on_query_error()
        {
            Thread.Sleep(50);
            _soapCaller.Setup(_ => _.SendRequest(It.IsAny<IWemoCommand>())).Throws<Exception>();
            _device.GetTodaysConsumption(CancellationToken.None).Wait();
            Logger.Verify(_ => _.RaiseErrorOnGettingData(It.IsAny<Exception>(), _device), Times.Once);
        }

        [SetUp]
        public void ScenarioSetup()
        {
            _soapCaller = new Mock<ISoapCaller>(MockBehavior.Strict);
            _soapCaller.Setup(_ => _.SendRequest(It.IsAny<GetInsightParamsWemoCommand>())).ReturnsAsync(_response);
            _soapCaller.Setup(_ => _.SendRequest(It.IsAny<SetInsightStateCommand>())).ReturnsAsync(string.Empty);
            _device = new WemoDevice(new DeviceDescription(), _soapCaller.Object, Logger.Object);
        }
    }
}
