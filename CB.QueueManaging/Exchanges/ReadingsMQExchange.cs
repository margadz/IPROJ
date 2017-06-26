using CB.Common.Config;
using CB.Common.Interfaces;
using RabbitMQ.Client;
using System;
using System.Threading.Tasks;

namespace CB.QueueManaging.Exchanges
{
    public class ReadingsMQExchange : IQueueWriter
    {
        private const string _routingKey = "raw_readings";
        private bool _disposed = false;
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;

        public ReadingsMQExchange()
        {
            _factory = new ConnectionFactory()
            {
                HostName = MQServerConfig.IP,
                Port = MQServerConfig.PORT,
                UserName = MQServerConfig.USER,
                Password = MQServerConfig.PASS,
                VirtualHost = MQServerConfig.VHOST
            };

            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public async Task Put(string message)
        {
            var body = MQServerConfig.ENCODING.GetBytes(message);

            await Task.Run(() => _channel.BasicPublish(exchange: MQServerConfig.READINGEXCHANGE, routingKey: _routingKey, basicProperties: null, body: body));
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

    }
}
