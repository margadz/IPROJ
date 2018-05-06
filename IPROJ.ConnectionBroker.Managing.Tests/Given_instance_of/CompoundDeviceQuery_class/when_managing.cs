using System.Threading;
using System.Threading.Tasks;
using IPROJ.ConnectionBroker.Managing.Quering;
using IPROJ.Contracts.Messaging;
using Moq;
using NUnit.Framework;

namespace Given_instance_of.CompoundDeviceQuery_class
{
    [TestFixture]
    public class when_managing
    {
        private Mock<IDeviceQuery> _firstManager;
        private Mock<IDeviceQuery> _secondManager;
        private CompoundDeviceQuery _manager;
        private CancellationTokenSource _tokentSource;

        public void TheTest()
        {
            Task.Factory.StartNew(async () => await _manager.QueryDevices(_tokentSource.Token), _tokentSource.Token);
            Task.Delay(10, _tokentSource.Token).Wait();
        }

        [Test]
        public void Should_call_first_manager()
        {
            TheTest();
            _firstManager.Verify(_ => _.QueryDevices(_tokentSource.Token), Times.Once);
        }

        [Test]
        public void Should_call_second_manager()
        {
            TheTest();
            _secondManager.Verify(_ => _.QueryDevices(_tokentSource.Token), Times.Once);
        }

        [SetUp]
        public void ScenarioSetup()
        {
            _tokentSource = new CancellationTokenSource();
            _firstManager = new Mock<IDeviceQuery>(MockBehavior.Strict);
            _secondManager = new Mock<IDeviceQuery>(MockBehavior.Strict);
            _firstManager.Setup(_ => _.QueryDevices(_tokentSource.Token)).Returns(Task.FromResult(0));
            _secondManager.Setup(_ => _.QueryDevices(_tokentSource.Token)).Returns(Task.FromResult(0));
            _manager = new CompoundDeviceQuery(new[] { _firstManager.Object, _secondManager.Object });
        }

        [TearDown]
        public void ScenarioTearDown()
        {
            _tokentSource?.Cancel();
            _tokentSource?.Dispose();
        }
    }
}
