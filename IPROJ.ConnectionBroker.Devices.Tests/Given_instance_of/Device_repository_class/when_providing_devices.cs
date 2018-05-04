using System;
using System.Linq;
using FluentAssertions;
using IPROJ.ConnectionBroker.DevicesManager;
using IPROJ.ConnectionBroker.DevicesManager.HS110;
using IPROJ.ConnectionBroker.DevicesManager.Wemo;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.DataRepository;
using IPROJ.Contracts.Logging;
using Moq;
using NUnit.Framework;

namespace Given_instance_of.Device_repository_class
{
    [TestFixture]
    public class when_providing_devices
    {
        private static DeviceDescription _wemoDeviceDescription = new DeviceDescription() { DeviceId = Guid.NewGuid(), TypeOfDevice = DeviceType.WEMO };
        private static DeviceDescription _hs110DeviceDescription = new DeviceDescription() { DeviceId = Guid.NewGuid(), Host = "192.0.0.1:111", TypeOfDevice = DeviceType.HS110 };
        private WemoDevice _wemoDevice = new WemoDevice(_wemoDeviceDescription, new Mock<IDeviceLog>().Object);
        private HS110Device _hs110Device = new HS110Device(_hs110DeviceDescription, new Mock<IDeviceLog>().Object);
        private Mock<IDataRepository> _dataRepositoryMock;
        private DeviceRepository _deviceRepository;

        [Test]
        public void Shuould_provide_wemo_device()
        {
            _deviceRepository.Devices.FirstOrDefault(_ => _.DeviceName == "Wemo" && _.DeviceId == _wemoDeviceDescription.DeviceId).Should().NotBeNull();
        }

        [Test]
        public void Shuould_provide_hs110_device()
        {
            _deviceRepository.Devices.FirstOrDefault(_ => _.DeviceName == "HS110" && _.DeviceId == _hs110DeviceDescription.DeviceId).Should().NotBeNull();
        }

        [Test]
        public void Should_call_data_repository()
        {
            var devices = _deviceRepository.Devices;

            _dataRepositoryMock.Verify(_ => _.GetAllDevicesAsync(), Times.Once);
        }

        [SetUp]
        public void ScenarioSetup()
        {
            _dataRepositoryMock = new Mock<IDataRepository>(MockBehavior.Strict);
            _dataRepositoryMock.Setup(_ => _.GetAllDevicesAsync()).ReturnsAsync(new[] { _wemoDeviceDescription, _hs110DeviceDescription });
            _deviceRepository = new DeviceRepository(_dataRepositoryMock.Object, new Mock<IDeviceLog>().Object);
        }

        [TearDown]
        public void ScenarioCleanup()
        {
            _deviceRepository.Dispose();
        }
    }
}
