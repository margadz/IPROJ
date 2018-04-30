using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.ConnectionBroker.Devices.Managing;
using Moq;
using NUnit.Framework;

namespace Given_instance_of.CompoundDeviceManager_class
{
    [TestFixture]
    public class when_managing
    {
        private Mock<IDeviceManager> _firstManager;
        private Mock<IDeviceManager> _secondManager;
        private CompoundDeviceManager _manager;
        private CancellationTokenSource _tokentSource;

        public void TheTest()
        {
            Task.Factory.StartNew(async () => await _manager.ManageDevices(_tokentSource.Token), _tokentSource.Token);
            Task.Delay(10, _tokentSource.Token).Wait();
        }

        [Test]
        public void Should_call_first_manager()
        {
            TheTest();
            _firstManager.Verify(_ => _.ManageDevices(_tokentSource.Token), Times.Once);
        }

        [Test]
        public void Should_call_second_manager()
        {
            TheTest();
            _secondManager.Verify(_ => _.ManageDevices(_tokentSource.Token), Times.Once);
        }

        [SetUp]
        public void ScenarioSetup()
        {
            _tokentSource = new CancellationTokenSource();
            _firstManager = new Mock<IDeviceManager>(MockBehavior.Strict);
            _secondManager = new Mock<IDeviceManager>(MockBehavior.Strict);
            _firstManager.Setup(_ => _.ManageDevices(_tokentSource.Token)).Returns(Task.FromResult(0));
            _secondManager.Setup(_ => _.ManageDevices(_tokentSource.Token)).Returns(Task.FromResult(0));
            _manager = new CompoundDeviceManager(new[] { _firstManager.Object, _secondManager.Object });
        }

        [TearDown]
        public void ScenarioTearDown()
        {
            _tokentSource?.Cancel();
            _tokentSource?.Dispose();
        }
    }
}
