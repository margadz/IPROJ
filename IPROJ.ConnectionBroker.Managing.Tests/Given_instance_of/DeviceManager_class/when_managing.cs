using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.ConnectionBroker.DevicesManager;
using IPROJ.ConnectionBroker.Managing;
using IPROJ.ConnectionBroker.Managing.Quering;
using IPROJ.Contracts;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.Device.Discovery;
using IPROJ.Contracts.Messaging;
using Moq;
using NUnit.Framework;

namespace Given_instance_of.DeviceManager_class
{
    [TestFixture]
    public class when_managing
    {
        private Guid _firstId = Guid.NewGuid();
        private Guid _secondId = Guid.NewGuid();
        private Mock<IDevice> _firstDevice;
        private Mock<IDevice> _secondDevice;
        private Mock<IDeviceQuery> _query;
        private Mock<IMessenger> _messenger;
        private Mock<IDeviceFinder> _finder;
        private Mock<IDeviceRepository> _repository;
        private DeviceManager _manager;
        
        public void TheTest(bool withEvent)
        {
            using (var source = new CancellationTokenSource(20000))
            {
                Task.Run(() => _manager.Manage(source.Token), source.Token);
                if (withEvent)
                {
                    _messenger.Raise(_ => _.OnDeviceDiscoveryRequest += null, new EventArgs());
                    _messenger.Raise(_ => _.OnDeviceDiscoveryRequest += null, new EventArgs());
                    _messenger.Raise(_ => _.OnStateSetChangeRequest += (name, arg) => { }, null, new DeviceReading() { DeviceId = _firstId, DeviceState = DeviceState.Off });
                }
                Thread.Sleep(150);
            }
        }

        [Test]
        public void Should_call_query()
        {
            TheTest(false);
            _query.Verify(_ => _.QueryDevices(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void Should_find_devices_on_request()
        {
            TheTest(true);
            _finder.Verify(_ => _.Discover(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void Should_set_state_of_correct_device()
        {
            TheTest(true);
            _firstDevice.Verify(_ => _.SetState(It.IsAny<DeviceState>()), Times.Once);
        }

        [Test]
        public void Should_set_correct_state_of_correct_device()
        {
            TheTest(true);
            _firstDevice.Verify(_ => _.SetState(DeviceState.Off), Times.Once);
        }

        [Test]
        public void Should_not_set_state_of_different_device()
        {
            TheTest(true);
            _secondDevice.Verify(_ => _.SetState(It.IsAny<DeviceState>()), Times.Never);
        }

        [SetUp]
        public void SceanrioSetup()
        {
            _query = new Mock<IDeviceQuery>(MockBehavior.Strict);
            _query.Setup(_ => _.QueryDevices(It.IsAny<CancellationToken>())).Returns(Task.FromResult(0));
            _messenger = new Mock<IMessenger>(MockBehavior.Strict);
            _messenger.Setup(_ => _.SendNewDevices(It.IsAny<IEnumerable<DeviceDescription>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(0));
            _finder = new Mock<IDeviceFinder>(MockBehavior.Strict);
            _finder.Setup(_ => _.Discover(It.IsAny<CancellationToken>())).ReturnsAsync(Array.Empty<DeviceDescription>());
            _repository = new Mock<IDeviceRepository>(MockBehavior.Strict);
            _firstDevice = new Mock<IDevice>(MockBehavior.Strict);
            _firstDevice.Setup(_ => _.SetState(It.IsAny<DeviceState>())).Returns(Task.FromResult(0));
            _firstDevice.SetupGet(_ => _.DeviceId).Returns(_firstId);
            _secondDevice = new Mock<IDevice>(MockBehavior.Strict);
            _secondDevice.Setup(_ => _.SetState(It.IsAny<DeviceState>())).Returns(Task.FromResult(0));
            _secondDevice.SetupGet(_ => _.DeviceId).Returns(_secondId);
            _repository.SetupGet(_ => _.Devices).Returns(new[] { _firstDevice.Object, _secondDevice.Object });
            _manager = new DeviceManager(_query.Object, _messenger.Object, _finder.Object, _repository.Object);
        }
    }
}
