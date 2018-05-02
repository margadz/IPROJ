using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.Configuration.Configurations;
using IPROJ.Contracts;
using IPROJ.Contracts.ConfigurationProvider;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.Helpers;
using IPROJ.Contracts.Logging;
using IPROJ.QueueManager.Connection;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace IPROJ.QueueManager.RabbitMQ
{
    public class RabbitMqWriter : IQueueWriter
    {
        private readonly string _routingKey;
        private readonly string _readingsExchange;
        private readonly Encoding _encoding;
        private readonly IQueueLogger _logger;
        private readonly IConnectionFactoryProvider _connectionFactoryProvider;
        private bool _disposed = false;
        private IConnection _connection;
        private IModel _channel;
        private bool _isConnected;

        public RabbitMqWriter(IConnectionFactoryProvider queueConnectionProvider, IConfigurationProvider configurationProvider, IQueueLogger logger)
        {
            Argument.OfWichValueShoulBeProvided(queueConnectionProvider, nameof(queueConnectionProvider));
            Argument.OfWichValueShoulBeProvided(configurationProvider, nameof(configurationProvider));
            Argument.OfWichValueShoulBeProvided(logger, nameof(logger));

            _connectionFactoryProvider = queueConnectionProvider;
            _routingKey = configurationProvider.GetOption(CoreConfigurations.Category, CoreConfigurations.RoutingKey);
            _readingsExchange = configurationProvider.GetOption(CoreConfigurations.Category, CoreConfigurations.ReadingsExchange);
            _encoding = Encoding.GetEncoding(int.Parse(configurationProvider.GetOption(CoreConfigurations.Category, CoreConfigurations.CodePage)));
            _logger = logger;
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        public Task Put(IEnumerable<DeviceReading> messages, CancellationToken cancellationToken)
        {
            if (!messages.Any())
            {
                return Task.FromResult(0);
            }

            try
            {
                Connect();
                _isConnected = true;
            }
            catch (Exception error)
            {
                _logger.RaiseOnQueueServerConnection(error);
                _connection?.Dispose();
                _channel?.Dispose();
                return Task.FromResult(0);
            }

            var json = JsonConvert.SerializeObject(messages);
            var body = _encoding.GetBytes(json);

            _channel.BasicPublish(exchange: _readingsExchange, routingKey: _routingKey, basicProperties: null, body: body);
            return Task.FromResult(0);
        }

        private void Connect()
        {
            if (_isConnected)
            {
                return;
            }

            _connection = _connectionFactoryProvider.CreateConnection();
            _channel = _connection.CreateModel();
        }

        private void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                _connection?.Dispose();
                _channel?.Dispose();
            }

            _disposed = true;
        }
    }
}
