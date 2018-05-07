using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.ConnectionBroker.Managing;
using IPROJ.ConnectionBroker.Managing.Quering;
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
        private Mock<IDeviceQuery> _query;
        private Mock<IMessenger> _messenger;
        private Mock<IDeviceFinder> _finder;
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

        [SetUp]
        public void SceanrioSetup()
        {
            _query = new Mock<IDeviceQuery>(MockBehavior.Strict);
            _query.Setup(_ => _.QueryDevices(It.IsAny<CancellationToken>())).Returns(Task.FromResult(0));
            _messenger = new Mock<IMessenger>(MockBehavior.Strict);
            _messenger.Setup(_ => _.SendNewDevices(It.IsAny<IEnumerable<DeviceDescription>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(0));
            _finder = new Mock<IDeviceFinder>(MockBehavior.Strict);
            _finder.Setup(_ => _.Discover(It.IsAny<CancellationToken>())).ReturnsAsync(Array.Empty<DeviceDescription>());
            _manager = new DeviceManager(_query.Object, _messenger.Object, _finder.Object);
            
        }
    }
}
