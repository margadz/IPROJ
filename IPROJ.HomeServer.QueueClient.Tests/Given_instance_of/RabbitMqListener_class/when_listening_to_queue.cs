using FluentAssertions;
using IPROJ.Configuration.Configurations;
using IPROJ.Contracts.ConfigurationProvider;
using IPROJ.Contracts.Logging;
using IPROJ.HomeServer.QueueClient;
using IPROJ.QueueManager.Connection;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;

namespace Given_instance_of.RabbitMqListener_class
{
    [TestFixture]
    public class when_listening_to_queue
    {
        private Mock<IConfigurationProvider> _configurationProvider;
        private Mock<IQueueLogger> _logger;
        private Mock<IConnectionFactoryProvider> _factory;
        private RabbitMqListener _listener;

        [Test]
        public void Should_log_when_connection_cannot_be_established()
        {
            _logger.Verify(_ => _.RaiseOnQueueServerConnection(It.IsAny<Exception>()), Times.Once);
        }

        [SetUp]
        public void ScenarioSetup()
        {
            _configurationProvider = new Mock<IConfigurationProvider>(MockBehavior.Strict);
            _configurationProvider.Setup(_ => _.GetOption(CoreConfigurations.Category, CoreConfigurations.ReadingsQueue)).Returns(string.Empty);
            _configurationProvider.Setup(_ => _.GetOption(CoreConfigurations.Category, CoreConfigurations.CodePage)).Returns("0");
            _logger = new Mock<IQueueLogger>(MockBehavior.Strict);
            _logger.Setup(_ => _.RaiseOnQueueServerConnection(It.IsAny<Exception>()));
            _factory = new Mock<IConnectionFactoryProvider>(MockBehavior.Strict);
            _factory.Setup(_ => _.CreateConnection()).Throws<Exception>();
            _listener = new RabbitMqListener(_factory.Object, _configurationProvider.Object, _logger.Object);
            _listener.Listen(CancellationToken.None);
        }
    }
}
