using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPROJ.Configuration.ConfigurationProvider;
using IPROJ.Configuration.Configurations;
using IPROJ.Contracts.DataModel;
using IPROJ.QueueManager;
using IPROJ.QueueManager.Connection;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace CB.QueueManaging.Exchanges
{
    public class ReadingsMQExchange : IQueueWriter
    {
        private readonly string _routingKey;
        private readonly string _readingsExchange;
        private readonly Encoding _encoding;
        private bool _disposed = false;
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;

        public ReadingsMQExchange(IConnectionFactoryProvider queueConnectionProvider, IConfigurationProvider configurationProvider)
        {
            _factory = queueConnectionProvider.ProvideFactory();
            _routingKey = configurationProvider.GetOption(CoreConfigurations.Category, CoreConfigurations.RoutingKey);
            _readingsExchange = configurationProvider.GetOption(CoreConfigurations.Category, CoreConfigurations.ReadingsExchange);
            _encoding = Encoding.GetEncoding(int.Parse(configurationProvider.GetOption(CoreConfigurations.Category, CoreConfigurations.CodePage)));
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
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

        public async Task Put(IEnumerable<DeviceReading> messages)
        {
            if (messages.Count() == 0)
            {
                return;
            }

            string json = JsonConvert.SerializeObject(messages);

            var body = _encoding.GetBytes(json);

            await Task.Run(() => _channel.BasicPublish(exchange: _readingsExchange, routingKey: _routingKey, basicProperties: null, body: body));
        }
    }
}
