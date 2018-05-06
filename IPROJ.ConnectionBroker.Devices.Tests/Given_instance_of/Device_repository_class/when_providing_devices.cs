using System;
using IPROJ.ConnectionBroker.DevicesManager;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.DataRepository;
using Moq;
using NUnit.Framework;
using IPROJ.Contracts.Devices;
using IPROJ.Contracts;

namespace Given_instance_of.Device_repository_class
{
    [TestFixture]
    public class when_providing_devices
    {
        private static DeviceDescription _wemoDeviceDescription = new DeviceDescription() { DeviceId = Guid.NewGuid(), TypeOfDevice = DeviceType.WEMO };
        private static DeviceDescription _hs110DeviceDescription = new DeviceDescription() { DeviceId = Guid.NewGuid(), Host = "192.0.0.1:111", TypeOfDevice = DeviceType.HS110 };
        private Mock<IDataRepository> _dataRepositoryMock;
        private Mock<IDeviceFactory> _factory;
        private DeviceRepository _deviceRepository;

        [Test]
        public void Shuould_call_factory()
        {
            var devices = _deviceRepository.Devices;

            _factory.Verify(_ => _.CreateDevice(It.IsAny<DeviceDescription>()), Times.Exactly(2));
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
            _factory = new Mock<IDeviceFactory>(MockBehavior.Strict);
            _factory.Setup(_ => _.CreateDevice(It.IsAny<DeviceDescription>())).Returns(new Mock<IDevice>().Object);
            _dataRepositoryMock = new Mock<IDataRepository>(MockBehavior.Strict);
            _dataRepositoryMock.Setup(_ => _.GetAllDevicesAsync()).ReturnsAsync(new[] { _wemoDeviceDescription, _hs110DeviceDescription });
            _deviceRepository = new DeviceRepository(_dataRepositoryMock.Object, _factory.Object);
        }

        [TearDown]
        public void ScenarioCleanup()
        {
            _deviceRepository.Dispose();
        }
    }
}
