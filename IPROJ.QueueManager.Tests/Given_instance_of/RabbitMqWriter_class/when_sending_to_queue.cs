using IPROJ.Configuration.Configurations;
using IPROJ.Contracts.ConfigurationProvider;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.Logging;
using IPROJ.QueueManager.Connection;
using IPROJ.QueueManager.RabbitMQ;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;

namespace Given_instance_of.RabbitMqListener_class
{
    [TestFixture]
    public class when_sending_to_queue
    {
        private Mock<IConfigurationProvider> _configurationProvider;
        private Mock<IQueueLogger> _logger;
        private Mock<IRabbitMqConnectionFactory> _factory;
        private RabbitMqWriter _writer;

        [Test]
        public void Should_log_when_connection_cannot_be_established()
        {
            _logger.Verify(_ => _.RaiseOnQueueServerConnection(It.IsAny<Exception>()), Times.Once);
        }

        [SetUp]
        public void ScenarioSetup()
        {
            _configurationProvider = new Mock<IConfigurationProvider>(MockBehavior.Strict);
            _configurationProvider.Setup(_ => _.GetOption(CoreConfigurations.Category, CoreConfigurations.ReadingsExchange)).Returns(string.Empty);
            _configurationProvider.Setup(_ => _.GetOption(CoreConfigurations.Category, CoreConfigurations.RoutingKey)).Returns(string.Empty);
            _configurationProvider.Setup(_ => _.GetOption(CoreConfigurations.Category, CoreConfigurations.CodePage)).Returns("0");
            _logger = new Mock<IQueueLogger>(MockBehavior.Strict);
            _logger.Setup(_ => _.RaiseOnQueueServerConnection(It.IsAny<Exception>()));
            _factory = new Mock<IRabbitMqConnectionFactory>(MockBehavior.Strict);
            _factory.Setup(_ => _.CreateConnection()).Throws<Exception>();
            _writer = new RabbitMqWriter(_factory.Object, _configurationProvider.Object, _logger.Object);
            _writer.Put(new[] { new DeviceReading() }, CancellationToken.None).Wait();
        }
    }
}
