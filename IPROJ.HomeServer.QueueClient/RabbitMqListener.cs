using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using IPROJ.Configuration.Configurations;
using IPROJ.Contracts.ConfigurationProvider;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.Helpers;
using IPROJ.Contracts.Logging;
using IPROJ.Contracts.Messaging;
using IPROJ.QueueManager.Connection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace IPROJ.HomeServer.QueueClient
{
    public class RabbitMqListener : IQueueListener
    {
        private readonly string _readingQueueName;
        private readonly Encoding _encoding;
        private readonly IQueueLogger _logger;
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;
        private bool _started = false;

        public RabbitMqListener(IConnectionFactoryProvider queueConnectionProvider, IConfigurationProvider configurationProvider, IQueueLogger logger)
        {
            Argument.OfWichValueShoulBeProvided(queueConnectionProvider, nameof(queueConnectionProvider));
            Argument.OfWichValueShoulBeProvided(configurationProvider, nameof(configurationProvider));
            Argument.OfWichValueShoulBeProvided(queueConnectionProvider, nameof(queueConnectionProvider));

            _readingQueueName = configurationProvider.GetOption(CoreConfigurations.Category, CoreConfigurations.ReadingsQueue);
            _encoding = Encoding.GetEncoding(int.Parse(configurationProvider.GetOption(CoreConfigurations.Category, CoreConfigurations.CodePage)));
            _factory = queueConnectionProvider.ProvideFactory();
            _logger = logger;
        }

        public event QueueEventHandler QueueEvent;

        public void Dispose()
        {
            _connection?.Dispose();
            _channel?.Dispose();
        }

        public void Listen(CancellationToken token)
        {
            if (_started)
            {
                return;
            }

            _started = true;

            try
            {
                _connection = _factory.CreateConnection();
                _logger.InformQueueServerHasBeenConnected();
                _channel = _connection.CreateModel();
                _logger.InformChannelHasBeenOpened();
            }
            catch (Exception error)
            {
                _logger.RaiseOnQueueServerConnection(error);
                return;
            }

            _channel.BasicQos(0, 1, false);
            var consumer = new EventingBasicConsumer(_channel);
            _channel.BasicConsume(queue: _readingQueueName, autoAck: true, consumer: consumer);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = _encoding.GetString(body);
                var result = JsonConvert.DeserializeObject<IEnumerable<DeviceReading>>(message);
                QueueEvent.Invoke(result);
            };

            while (!token.IsCancellationRequested)
            {
            }
        }
    }
}
