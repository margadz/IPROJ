using FluentAssertions;
using IPROJ.ConnectionBroker.Devices;
using IPROJ.ConnectionBroker.Devices.HS110;
using IPROJ.ConnectionBroker.DevicesManager.Wemo;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.Logging;
using Moq;
using NUnit.Framework;

namespace Given_instance_of.DeviceFactory_class
{
    [TestFixture]
    public class when_creating_devices
    {
        private DeviceFactory _factory;
        private DeviceDescription _description;

        [Test]
        public void Should_create_wemo_device()
        {
            _description.TypeOfDevice = DeviceType.WEMO;
            _factory.CreateDevice(_description).Should().BeAssignableTo<WemoDevice>();
        }

        [Test]
        public void Should_create_hs110_device()
        {
            _description.TypeOfDevice = DeviceType.HS110;
            _factory.CreateDevice(_description).Should().BeAssignableTo<HS110Device>();
        }

        [SetUp]
        public void ScenarioSetup()
        {
            _factory = new DeviceFactory(new Mock<IDeviceLogger>(MockBehavior.Strict).Object);
            _description = new DeviceDescription() { Host = "192.0.0.1:111" };
        }
    }
}
