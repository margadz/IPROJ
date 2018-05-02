using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.DataRepository;
using IPROJ.Contracts.Messaging;
using IPROJ.HomeServer.QueueClient;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Given_instance_of.MessagesHandler_class
{
    [TestFixture]
    public class when_handling_messages
    {
        private Mock<IDataRepository> _dataRepository;
        private Mock<IQueueListener> _queueListener;
        private Mock<IInstantMessenger> _messenger;
        private IEnumerable<DeviceReading> _instantReadings = new[] { new DeviceReading() { ReadingCharacter = ReadingCharacter.Instant }, new DeviceReading() { ReadingCharacter = ReadingCharacter.Instant } };
        private IEnumerable<DeviceReading> _dailyReadings = new[] { new DeviceReading() { ReadingCharacter = ReadingCharacter.Daily }, new DeviceReading() { ReadingCharacter = ReadingCharacter.Daily } };
        private MessagesHandler _handler;
        private CancellationTokenSource _tokenSource;

        public void TheTest()
        {
            Task.Factory.StartNew(() => _handler.StartStartHandling(_tokenSource.Token), _tokenSource.Token);
            _queueListener.Raise(_ => _.QueueEvent += null, _instantReadings.Concat(_dailyReadings).Concat(new DeviceReading[] { null, null }));
            Task.Delay(30).Wait();
        }
        
        [Test]
        public void Messenger_should_be_called()
        {
            _messenger.Verify(_ => _.SendMessage(It.IsAny<IEnumerable<DeviceReading>>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void Repository_should_be_called()
        {
            _dataRepository.Verify(_ => _.AddReadingsAsync(It.IsAny<IEnumerable<DeviceReading>>()), Times.Once);
        }

        [Test]
        public void Should_send_instant_reading_to_messenger()
        {
            _messenger
                .Verify(_ => _.SendMessage(It.Is<IEnumerable<DeviceReading>>(readings => readings.All(reading => reading != null && reading.ReadingCharacter == ReadingCharacter.Instant) && readings.Count() == 2), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void Should_send_daily_reading_to_repository()
        {
            _dataRepository
                .Verify(_ => _.AddReadingsAsync(It.Is<IEnumerable<DeviceReading>>(readings => readings.All(reading => reading != null && reading.ReadingCharacter == ReadingCharacter.Daily) && readings.Count() == 2)), Times.Once);
        }

        [SetUp]
        public void ScenarioSetup()
        {
            _tokenSource = new CancellationTokenSource();
            _dataRepository = new Mock<IDataRepository>(MockBehavior.Strict);
            _dataRepository.Setup(_ => _.AddReadingsAsync(It.IsAny<IEnumerable<DeviceReading>>())).Returns(Task.FromResult(0));
            _queueListener = new Mock<IQueueListener>(MockBehavior.Strict);
            _queueListener.Setup(_ => _.Listen(It.IsAny<CancellationToken>()));
            _messenger = new Mock<IInstantMessenger>(MockBehavior.Strict);
            _messenger.Setup(_ => _.SendMessage(It.IsAny<IEnumerable<DeviceReading>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(0));
            _handler = new MessagesHandler(_dataRepository.Object, _queueListener.Object, _messenger.Object);
            TheTest();
        }

        [TearDown]
        public void TearDown()
        {
            _tokenSource.Cancel();
            _tokenSource.Dispose();
        }
    }
}
